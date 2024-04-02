using System;
using System.Threading.Tasks;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;
using VaultSharp.V1.Commons;


namespace VaultDotnetExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // HashiCorp Vault sunucusunun adresini belirtin
            string vaultAddress = "";

            // HashiCorp Vault token'ınızı girin
            string vaultToken = "";

            try
            {  // VaultSharp istemci yapılandırması
                var vaultClientSettings = new VaultClientSettings(vaultAddress, new TokenAuthMethodInfo(vaultToken));

                // VaultSharp istemcisini oluştur
                IVaultClient vaultClient = new VaultClient(vaultClientSettings);

                // Vault'ta 'secret/myapp/config' adlı bir secret bulunduğunu varsayalım
                Secret<SecretData> secretResponse = await vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: "myapp", mountPoint: "secret");

                if (secretResponse != null && secretResponse.Data != null && secretResponse.Data.Data != null)
                {
                    var secretData = secretResponse.Data.Data;
                    foreach (var keyValue in secretData)
                    {
                        Console.WriteLine($"{keyValue.Key}: {keyValue.Value}");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Secret not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
