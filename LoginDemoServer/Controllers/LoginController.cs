
using LoginDemoServer.Models;
using LoginDemoServer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;



namespace LoginDemoServer.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //a variable to hold a reference to the db context!
       
        private LoginDemoDbContext context;

        //Use dependency injection to get the db context intot he constructor



        public LoginController(LoginDemoDbContext context)
        {
            this.context = context;
        }

        // POST api/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] DTO.LoginInfo loginDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Get model user class from DB with matching email. 
             
                Models.Users modelsUser = context.UserLogin(loginDto.Email,loginDto.Password);
                
                //Check if user exist for this email and if password match, if not return Access Denied (Error 403) 
                if (modelsUser == null || modelsUser.Password != loginDto.Password) 
                {
                    return Unauthorized();
                }

                //Login suceed! now mark login in session memory!
                HttpContext.Session.SetString("loggedInUser", modelsUser.Email);

                return Ok(new UsersDTO(modelsUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        

        // Get api/check
        [HttpGet("check")]
        public IActionResult Check()
        {
            try
            {
                //Check if user is logged in 
                string userEmail = HttpContext.Session.GetString("loggedInUser");

                if (string.IsNullOrEmpty(userEmail))
                {
                    return Unauthorized("User is not logged in");
                }


                //user is logged in - lets check who is the user
                Models.Users modelsUser = context.UserLogin(userEmail,"1234");
             
                return Ok(new DTO.UsersDTO(modelsUser));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("uploadProfile/{email}")]
        public async Task<IActionResult> UploadPhoto(string email, IFormFile file) 
        {
            //retrieve the User from the Db
            Users u = context.GetUSerFromDB(email);

            //if no user or file not good
            if (u == null)
                return NotFound("No such user Found");
            if (file==null||file.Length <= 0)
                return BadRequest("Invalid File");

            //Decide on File Name
            //System.IO.Path.GetExtension --> gets the type of the file .jpg, mp3,mp4 etc.
            string fileName = $"ProfileImage_{u.Email}{System.IO.Path.GetExtension(file.FileName)}";

            //the path to save the file in
            string path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);

            // Save the image to the server
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Update the user's Image field in the database
            u.Image = fileName;
            try
            {
                await context.SaveChangesAsync();
                return Ok(new { Message = "Profile image uploaded successfully.", FileName = fileName });
            }
            catch(Exception ex) { return BadRequest(ex.Message); }  





        }
    }
}
