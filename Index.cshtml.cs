using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dog_Store_App.Data;

namespace Dog_Store_App.Pages.myDog
{
    public class IndexModel : PageModel
    {
        private readonly Dog_Store_App.Data.ApplicationDbContext _context;

        public IndexModel(Dog_Store_App.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Dog> Dog { get;set; }

        public async Task OnGetAsync()
        {

            var dog = from m in _context.Dog select m;
            if (User.IsInRole ("executive"))
            {
               
                dog = dog.Where(s => s.Breeds.Contains("hybrid"));
               
            }else if (User.IsInRole("markerter"))
            {
                dog = dog.Where(s => s.DogName.Contains("bingo"));
            }else if (User.IsInRole("general manager"))
            {
                dog = dog.Where(s => s.color.Contains("white"));
            }
            else
            {
                dog = dog.Where(s => s.Breeds.Contains(""));
            }; 
            Dog = await dog.ToListAsync();

        }
    }
}
