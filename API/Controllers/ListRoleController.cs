using DAL.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Services.DTO;
using Services.IService;

namespace API.Controllers
{
    [Route("ListRole")]
    //[Authorize]
    [Produces("application/json")]

    [EnableCors("CORSPolicy")]
    [ApiController]
    public class ListListRoleController : ControllerBase
    {
        private readonly IServiceAsync<ListRole, ListRoleDto> _service;
        private readonly Serilog.ILogger _logger;
        public ListListRoleController(IServiceAsync<ListRole, ListRoleDto> service,
                     Serilog.ILogger logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("AddListListRole")]
        public async Task<IActionResult> AddListListRole([FromBody] ListRoleDto dto)
        {
            try
            {
                await _service.Add(dto);
                return Ok("ListRole ajouté avec succès !");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetListRoles")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListRoleDto>>> GetListRoles()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var lst = _service.GetAll();
                var lstUsr = lst.ToList();

                if (lstUsr.Count != 0)
                {
                    return new OkObjectResult(lstUsr);
                }
                else
                {
                    var showmessage = "Pas d'element dans la liste";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);

                }

            }
            catch (Exception ex)
            {

                _logger.Error("Erreur GetListRoles <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        [Route("GetListRole")]
        [HttpGet]
        public async Task<ActionResult<ListRoleDto>> GetListRole(int ListRoleId) // Change string to int
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                var usr = await _service.GetById(ListRoleId).ConfigureAwait(false); // Use int

                if (usr != null)
                {
                    return Ok(usr);
                }
                else
                {
                    var showmessage = "ListRole inexistante";
                    dict.Add("Message", showmessage);
                    return NotFound(dict);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur GetListRole <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }


        //// GET: api/ListRole/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ListRoleDto>> GetListRole(int id)
        //{
        //    var ListRole = await _service.GetById(id);

        //    if (ListRole == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(ListRole);
        //}

        // POST: api/ListRole
      
       


        // PUT: api/ListRole/5
        //[HttpPut("{id}")]
        [Route("UpdListRole")]
        [HttpPut]
        public async Task<ActionResult> ModifListRole(ListRoleDto ListRole)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Update(ListRole).ConfigureAwait(false);

                var showmessage = "Modification effectuee avec succes";
                dict.Add("Message", showmessage);
                return Ok(dict);


            }
            catch (Exception ex)
            {

                _logger.Error("Erreur ModifListRole <==> " + ex.ToString());
                var showmessage = "Erreur" + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }

        [Route("DelListRole")]
        [HttpDelete]
        public async Task<ActionResult> DeletListRole(int ListRoleId) // Change string to int
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                await _service.Delete(ListRoleId).ConfigureAwait(false);

                var showmessage = "ListRole supprimée avec succès";
                dict.Add("Message", showmessage);
                return Ok(dict);
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur DeletListRole <==> " + ex.ToString());
                var showmessage = "Erreur: " + ex.Message;
                dict.Add("Message", showmessage);
                return BadRequest(dict);
            }
        }
    }

}

