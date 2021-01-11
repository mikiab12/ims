using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ims.data.Entities;
using ims.domain.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace ims.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigurationController : BaseController
    {
        public IConfigurationFacade _confFacade;
        public ConfigurationController(IConfigurationFacade confFacade)
        {
            _confFacade = confFacade;
        }

        [HttpPost]
        public IActionResult AddColor([FromBody] Color clr)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.AddColor(clr);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateColor([FromBody] Color clr)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.UpdateColor(clr);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult DeleteColor(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                 _confFacade.DeleteColor(id);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetColor(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetColor(id);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllColors()
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetAllColors();
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

       [HttpPost]
        public IActionResult AddStore([FromBody] Store store)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.AddStore(store);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateStore([FromBody] Store store)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.UpdateStore(store);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult DeleteStore(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                 _confFacade.DeleteStore(id);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetStore(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetStore(id);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllStores()
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetAllStores();
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

       [HttpPost]
        public IActionResult AddFactory([FromBody] Factory fact)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.AddFactory(fact);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateFactory([FromBody] Factory fact)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.UpdateFactory(fact);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult DeleteFactory(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                 _confFacade.DeleteFactory(id);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetFactory(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetFactory(id);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllFactories()
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetAllFactories();
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

       [HttpPost]
        public IActionResult AddSole([FromBody] Sole sole)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.AddSole(sole);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateSole([FromBody] Sole sole)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.UpdateSole(sole);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult DeleteSole(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                 _confFacade.DeleteSole(id);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetSole(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetSole(id);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllSoles()
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetAllSoles();
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }
    
       [HttpPost]
        public IActionResult AddSize([FromBody] Size size)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.AddSize(size);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateSize([FromBody] Size size)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.UpdateSize(size);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult DeleteSize(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                 _confFacade.DeleteSize(id);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetSize(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetSize(id);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllSizes()
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetAllSizes();
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

       [HttpPost]
        public IActionResult AddShop([FromBody] Shop shop)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.AddShop(shop);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateShop([FromBody] Shop shop)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.UpdateShop(shop);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult DeleteShop(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                 _confFacade.DeleteShop(id);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetShop(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetShop(id);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllShops()
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetAllShops();
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

       [HttpPost]
        public IActionResult AddMachineCode([FromBody] MachineCode mc)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.AddMachineCode(mc);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public IActionResult UpdateMachineCode([FromBody] MachineCode mc)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.UpdateMachineCode(mc);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult DeleteMachineCode(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                 _confFacade.DeleteMachineCode(id);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetMachineCode(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetMachineCode(id);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllMachineCodes()
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetAllMachineCodes();
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpGet]
        public IActionResult GetBaseData()
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetInitDataModel();
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

        [HttpPost]
        public  IActionResult AddSupplier(Supplier supplier)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.AddSupplier(supplier);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
                
            }
        }

        [HttpPost]
        public IActionResult UpdateSupplier(Supplier supplier)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.UpdateSupplier(supplier);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
                
            }
        }

        [HttpGet]
        public IActionResult DeleteSupplier(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                _confFacade.DeleteSupplier(id);
                return SuccessfulResponse(true);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
                
            }
        }

        [HttpGet]
        public IActionResult GetSupplier(int id)
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetSupplier(id);
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }
        
        [HttpGet]
        public  IActionResult GetAllSuppliers()
        {
            try
            {
                _confFacade.SetSession(GetSession());
                var resp = _confFacade.GetAllSuppliers();
                return SuccessfulResponse(resp);
            }
            catch (Exception ex)
            {
                return ErrorResponse(ex);
            }
        }

    }
}