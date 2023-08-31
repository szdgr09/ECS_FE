using Supabase;

namespace ERS_FE_.Services
{
    public interface ISupabaseService
    {
        Client Client { get; }
    }
}
