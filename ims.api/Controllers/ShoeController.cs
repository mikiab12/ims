using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ims.data.Entities;
using ims.domain.Shoes;
using Microsoft.AspNetCore.Mvc;

namespace ims.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShoeController : BaseController
    {

        private IShoeFacade _shoeFacade;
        public ShoeController(IShoeFacade shoeFacade)
        {
            _shoeFacade = shoeFacade;
        }

        [HttpGet]
        public IActionResult GetShoeModel(int id)
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                var resp = _shoeFacade.GetShoeModel(id);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetShoe(int id)
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                var resp = _shoeFacade.GetShoe(id);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllShoeModels()
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                var resp = _shoeFacade.GetAllShoeModels();
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllShoes()
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                var response = _shoeFacade.GetAllShoes();
                return SuccessfulResponse(response);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }


        [HttpGet]
        public  IActionResult GetShoeWithModel(int modelId)
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                var resp = _shoeFacade.GetShoeWithModel(modelId);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult CreateShoe(Shoe sh)
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                var resp = _shoeFacade.CreateShoe(sh);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult CreateShoeModel(ShoeModel model)
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                var resp = _shoeFacade.CreateShoeModel(model);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateShoeModel(ShoeModel Model)
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                var resp = _shoeFacade.UpdateShoeModel(Model);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public  IActionResult UpdateShoe(Shoe shoe)
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                var resp = _shoeFacade.UpdateShoe(shoe);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
                //throw;
            }
        }

        [HttpGet]
        public  IActionResult DeleteShoe(int id)
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                _shoeFacade.DeleteShoe(id);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult DeleteShoeModel(int id)
        {
            try
            {
                _shoeFacade.SetSession(GetSession());
                _shoeFacade.DeleteShoeModel(id);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }
    }
}