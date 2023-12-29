using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Microsoft.AspNetCore.Identity;

namespace FUploader.Core.FireBase
{
    public class FirebaseAuth
    {
  
        private FirebaseAuthClient? _client;

        
        public FirebaseAuth(IConfiguration configuration)
        {
            init(configuration);
        }

        private void init(IConfiguration configuration) {
            var apiKey = configuration.GetSection("Firebase").GetValue<string>("apiKey") ?? "";
            var authDomain = configuration.GetSection("Firebase").GetValue<string>("authDomain") ?? "";

            if (apiKey.Length == 0)
            {
                _client = null;
            }
            var config = new FirebaseAuthConfig
            {
                ApiKey = apiKey,
                AuthDomain = authDomain,
                Providers = new FirebaseAuthProvider[]
                    {
                        new EmailProvider()
                    }

            };
            _client = new FirebaseAuthClient(config);
        }


        public async Task<string> Loggin(string email, string password) {


            if (_client is null) {
                throw new Exception("Bad firebase settings API key is empty");
            }

            var userCredential = await _client.SignInWithEmailAndPasswordAsync(email, password);
            var token = await userCredential.User.GetIdTokenAsync();
            
            return token;
        }
    }
}
