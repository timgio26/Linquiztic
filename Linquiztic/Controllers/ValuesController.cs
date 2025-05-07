using Linquiztic.Data;
using Linquiztic.Dtos;
using Linquiztic.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Linquiztic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController(MyDbContext context) : ControllerBase
    {
        private readonly MyDbContext _context = context;

        //[HttpGet]
        //public async Task<ActionResult<List<User>>> AllCourse()
        //{
        //    var alluser = await _context.Users.Include(user => user.Words).ToListAsync();
        //    return Ok(alluser);
        //}

        [HttpPost("signup")]
        public async Task<ActionResult> AddUser(UserDto request)
        {
            var selectedUser = await _context.Users.FirstOrDefaultAsync(each => each.Email == request.email);
            if (selectedUser is not null) return BadRequest("user exist");
            User newuser = new User()
            {
                Id = Guid.NewGuid(),
                Name = request.name,
                Email = request.email
            };
            await _context.Users.AddAsync(newuser);
            await _context.SaveChangesAsync();
            return Ok(newuser);
        }

        [HttpPost("signin")]
        public async Task<ActionResult> Signin(SigninDto request)
        {
            var selectedUser = await _context.Users.Include(user=>user.UserLanguages).FirstOrDefaultAsync(each => each.Email == request.email);
            if (selectedUser is null) return BadRequest("user exist");
            return Ok(selectedUser);
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var selectedUser = await _context.Users.FirstOrDefaultAsync(each => each.Id == id);
            if (selectedUser is null) return BadRequest("no user");
            _context.Users.Remove(selectedUser);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("addLanguage")]
        public async Task<ActionResult> AddLanguage(AddLanguageDto request)
        {
            var selectedUser = await _context.Users.FirstOrDefaultAsync(each => each.Id == request.UserId);
            if (selectedUser is null) return BadRequest("no user");
            UserLanguage newLanguage = new UserLanguage()
            {
                Language = request.Language,
                Level = request.Level,
                UserId = selectedUser.Id,
                User = selectedUser,
                Id = Guid.NewGuid()
            };
            Console.WriteLine(newLanguage);
            await _context.UserLanguages.AddAsync(newLanguage);
            await _context.SaveChangesAsync();
            return Ok(newLanguage);
        }

        //[HttpPost("word")]
        //public async Task<ActionResult> AddWord(AddWordDto request)
        //{
        //    var selectedUser = await _context.Users.FirstOrDefaultAsync(each => each.Id == request.UserId);
        //    if (selectedUser is null) return BadRequest("no user");
        //    Word newWord = new Word()
        //    {
        //        WordText = request.WordText,
        //        UserId = request.UserId,
        //        AddedDate = DateOnly.FromDateTime(DateTime.Now),
        //        User = selectedUser,
        //        Mastery = "new"
        //    };
        //    await _context.Words.AddAsync(newWord);
        //    await _context.SaveChangesAsync();
        //    return Ok(newWord);
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteWord(int id)
        //{
        //    var selectedWord = await _context.Words.FirstOrDefaultAsync(each => each.Id == id);
        //    if (selectedWord is null) return BadRequest();
        //    _context.Words.Remove(selectedWord);
        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}


        //[HttpGet("allwords")]
        //public async Task<ActionResult<List<Word>>> AllWords()
        //{
        //    var allWords = await _context.Words.ToListAsync();
        //    return Ok(allWords);
        //}
    }
}
