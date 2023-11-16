using tugasmod9.Data;
using tugasmod9.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace tugasmod9.Controllers
{
    [Route("api/UserAPI")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUser()
        {
            return Ok(UserStore.UserList);
        }

        [HttpGet("{Id:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]

        public ActionResult<UserDTO> GetUser(int id)
        {
            if (id == 0) return BadRequest();
            var acc = UserStore.UserList.FirstOrDefault(u => u.Id == id);
            if (acc == null) return NotFound();
            return Ok(acc);
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateAcc([FromBody] UserDTO UserDTO)
        {
            if (UserStore.UserList.FirstOrDefault(u => u.Username.ToLower() == UserDTO.Username.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "User already exists");
                return BadRequest(ModelState);
            }

            if (UserDTO == null)
            {
                return BadRequest(UserDTO);
            }

            if (UserDTO.Id != 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            UserDTO.Id = UserStore.UserList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            UserStore.UserList.Add(UserDTO);
            string response = "Sukses menambahkan data Akun" + "\nNama : " + UserDTO.Username;
            return CreatedAtRoute("GetUser", new { id = UserDTO.Id }, response);
        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var acc = UserStore.UserList.FirstOrDefault(u => u.Id == id);
            if (acc == null)
            {
                return NotFound();
            }
            UserStore.UserList.Remove(acc);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateUser")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO UserDTO)
        {
            if (UserDTO == null || id != UserDTO.Id)
            {
                return BadRequest();
            }
            var user = UserStore.UserList.FirstOrDefault(u => u.Id == id);
            user.Username = UserDTO.Username;
            user.Password = UserDTO.Password;
            return NoContent();
        }
    }
}
