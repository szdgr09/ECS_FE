using ERS_FE_.Contracts;
using ERS_FE_.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supabase;
using ERS_FE_.Models;

namespace ERS_FE_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly ISupabaseService _supabaseService;
        public NewsletterController(ISupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpPost]
        public async Task<ActionResult<NewsLetterResponse>> Get()
        {  
            try
            {
                var response = await _supabaseService.Client.From<Newsletter>().Where(n => n.Id == 1).Get();
                var newsletter = response.Models.FirstOrDefault();

                var newsLetterResponse = new NewsLetterResponse()
                {
                    Description = "test",
                    CreatedAt = DateTime.Now,
                    Name = "test",
                    Id = 1,
                };
                
                return Ok(newsLetterResponse);

            } catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            };

            return null; 
        }

    }
}
