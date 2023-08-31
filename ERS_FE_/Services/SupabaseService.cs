using Supabase;

namespace ERS_FE_.Services
{
    public class SupabaseService : ISupabaseService
    {
        public Client Client { get; }

        public SupabaseService(IConfiguration configuration) 
        {
            var supabaseUrl = configuration["SupabaseUrl"];
            var supabaseKey = configuration["SupabaseKey"];
            Client = new Client(supabaseUrl, supabaseKey, new SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = true,
            });
            Client.InitializeAsync();

        }
    }
}
