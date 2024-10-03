using FaceWelcome.Repository.FirebaseStorages.Models;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceWelcome.Repository.FirebaseStorages.Repositories
{
    public class FirebaseStorageRepository
    {
        public FirebaseStorageRepository()
        {
        }

        private FirebaseStorageModel GetFirebaseStorageProperties()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                                  .SetBasePath(Directory.GetCurrentDirectory())
                                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            return new FirebaseStorageModel()
            {
                ApiKey = configuration.GetSection("FirebaseStorage:apiKey").Value,
                Bucket = configuration.GetSection("FirebaseStorage:bucket").Value,
                AuthEmail = configuration.GetSection("FirebaseStorage:authEmail").Value,
                AuthPassword = configuration.GetSection("FirebaseStorage:authPassword").Value
            };
        }

        private async Task<FirebaseAuthLink> AuthenticateFirebaseAsync()
        {
            var firebaseStorage = GetFirebaseStorageProperties();
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(firebaseStorage.ApiKey));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(firebaseStorage.AuthEmail, firebaseStorage.AuthPassword);
            return auth;
        }

        private async Task<string> UploadImageToFirebase(string localImagePath, string folder)
        {
            try
            {
                var auth = await AuthenticateFirebaseAsync(); // Authenticate first
                //string bucketName = "face-image-cb4c4.appspot.com";  // Replace with your Firebase Storage bucket
                var firebaseStorageModel = GetFirebaseStorageProperties();

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                
                string objectName = $"{folder}/{Path.GetFileNameWithoutExtension(localImagePath)}_{timestamp}.jpg"; // Create unique name

                var stream = File.Open(localImagePath, FileMode.Open);
                var firebaseStorage = new FirebaseStorage(
                    firebaseStorageModel.Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken),
                        ThrowOnCancel = true
                    });

                var task = await firebaseStorage
                    .Child(objectName)
                    .PutAsync(stream);

                string imageUrl = task; // Get the image URL

                // Save the image URL to the SQL database
                
                return imageUrl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
