using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using UWP.Classes;

namespace UWP
{
    public sealed partial class Login : Page
    {
        const string clientID = "195955754145-1bap8vlotatf1bm4u9jqemjumn3s0fv5.apps.googleusercontent.com";
        const string redirectURI = "com.localhost:/oauth2redirect";
        const string authorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        const string tokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        const string userInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";
        private string authorizationRequest;
        ProgressRing progress;

        public Login()
        {

            this.InitializeComponent();
            Ring.IsActive = false;

        }
       
        private void Login_btn_OnClick(object sender, RoutedEventArgs e)
        {
            string state = randomDataBase64url(32);
            string code_verifier = randomDataBase64url(32);
            string code_challenge = base64urlencodeNoPadding(sha256(code_verifier));
            const string code_challenge_method = "S256";

            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["state"] = state;
            localSettings.Values["code_verifier"] = code_verifier;

            authorizationRequest = string.Format("{0}?response_type=code&scope=openid%20profile&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
                authorizationEndpoint,
                System.Uri.EscapeDataString(redirectURI),
                clientID,
                state,
                code_challenge,
                code_challenge_method);

            //output("Opening authorization request URI: " + authorizationRequest);
        
            var success = Windows.System.Launcher.LaunchUriAsync(new Uri(authorizationRequest));
          
           
            


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            if (e.Parameter is Uri)
            {
              
                Uri authorizationResponse = (Uri)e.Parameter;
                string queryString = authorizationResponse.Query;
                //output("MainPage received authorizationResponse: " + authorizationResponse);

                // ref: http://stackoverflow.com/a/11957114/72176
                Dictionary<string, string> queryStringParams =
                        queryString.Substring(1).Split('&')
                             .ToDictionary(c => c.Split('=')[0],
                                           c => Uri.UnescapeDataString(c.Split('=')[1]));

                if (queryStringParams.ContainsKey("error"))
                {
                    output(String.Format("OAuth authorization error: {0}.", queryStringParams["error"]));
                    return;
                }

                if (!queryStringParams.ContainsKey("code")
                    || !queryStringParams.ContainsKey("state"))
                {
                    output("Malformed authorization response. " + queryString);
                    return;
                }

                string code = queryStringParams["code"];
                string incoming_state = queryStringParams["state"];

                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                string expected_state = (String)localSettings.Values["state"];

                if (incoming_state != expected_state)
                {
                    output(String.Format("Received request with invalid state ({0})", incoming_state));
                    return;
                }

                localSettings.Values["state"] = null;

                //output(Environment.NewLine + "Authorization code: " + code);

                string code_verifier = (String)localSettings.Values["code_verifier"];
                performCodeExchangeAsync(code, code_verifier);
            }
            else
            {
                Debug.WriteLine(e.Parameter);
            }
       
        }

        async void performCodeExchangeAsync(string code, string code_verifier)
        {

            Ring.IsActive = true;
            string tokenRequestBody = string.Format(
                "code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&scope=&grant_type=authorization_code",
                code,
                System.Uri.EscapeDataString(redirectURI),
                clientID,
                code_verifier
            );
            StringContent content =
                new StringContent(tokenRequestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpClientHandler handler = new HttpClientHandler();
            handler.AllowAutoRedirect = true;
            HttpClient client = new HttpClient(handler);

            //output(Environment.NewLine + "Exchanging code for tokens...");

            HttpResponseMessage response = await client.PostAsync(tokenEndpoint, content);
            string responseString = await response.Content.ReadAsStringAsync();
            //output(responseString);

            if (!response.IsSuccessStatusCode)
            {
                output("Authorization code exchange failed.");
                return;
            }

            JsonObject tokens = JsonObject.Parse(responseString);
            string accessToken = tokens.GetNamedString("access_token");
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            //output("Making API Call to Userinfo...");
            HttpResponseMessage userinfoResponse = client.GetAsync(userInfoEndpoint).Result;
            //var info = await userinfoResponse.Content.ReadAsStringAsync();
            //JsonObject obj = JsonObject.Parse(info);
            string userinfoResponseContent = await userinfoResponse.Content.ReadAsStringAsync();
            output(userinfoResponseContent);
           
            UserInfo userInfo = Str2UserInfo(userinfoResponseContent);
            Util.PostUser(userInfo);

          

            
            if (userinfoResponseContent != null)
            {

                App.sub = userInfo.sub;
                App.name = userInfo.name;
                App.given_name = userInfo.given_name;
                App.family_name = userInfo.family_name;
                App.profile = userInfo.profile;
                App.picture = userInfo.picture;
                App.gender = userInfo.gender;
                App.locale = userInfo.locale;
                this.Frame.Navigate(typeof(MainScreen));

            }
        }

        public void output(string output)
        {
            Debug.WriteLine(output);
        }

        public static string randomDataBase64url(uint length)
        {
            IBuffer buffer = CryptographicBuffer.GenerateRandom(length);
            return base64urlencodeNoPadding(buffer);
        }

        public static IBuffer sha256(string inputStirng)
        {
            HashAlgorithmProvider sha = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha256);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(inputStirng, BinaryStringEncoding.Utf8);
            return sha.HashData(buff);
        }

        public static string base64urlencodeNoPadding(IBuffer buffer)
        {
            string base64 = CryptographicBuffer.EncodeToBase64String(buffer);
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            base64 = base64.Replace("=", "");
            return base64;
        }

        private void link_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                link.NavigateUri = new Uri(authorizationRequest);
            }
            catch
            {
            }
            
        }

        private UserInfo Str2UserInfo(string json)
        {
            return JsonConvert.DeserializeObject<UserInfo>(json);
        }
    }
}
