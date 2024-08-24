namespace WebMain.Controllers
{
    using AutoMapper;
    using FluentValidation;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Core.Dominio;
    using Core.Dominio.Entidades;
    using System.Security.Claims;
    using Tareo.Aplicacion.Dto;
    using Tareo.Dominio.Entidades;
    using Tareo.Dominio.Entidades.SCode.EntidadesReporte;
    using Tareo.Persistencia.Mapeadores;
    using Core.Dominio.Util;
    using System.Linq;
    using System.Data.Entity.Core.Objects;
    using System.Net.Http;
    using System.Net;
    using Newtonsoft.Json;
    using Microsoft.AspNet.SignalR.Hosting;

    public class ItemController : ApiController
    {
        private readonly IService<Item> ItemService;
        private readonly IValidator<ItemDto> ValidadorItemDto;
        private readonly IServiceReporte<ProyectoReporte> ProcedureService;

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()
            .DeclaringType);

        public ItemController(IValidator<ItemDto> ValidadorItemDto,
            IService<Item> ItemService,
            IServiceReporte<ProyectoReporte> ProcedureService)
        {
            if (ItemService == null) throw new ArgumentNullException("IService<ItemService>");
            if (ValidadorItemDto == null) throw new ArgumentNullException("IValidator<ValidadorItemDto>");

            this.ItemService = ItemService;
            this.ValidadorItemDto = ValidadorItemDto;
            this.ProcedureService = ProcedureService;
        }

        private HttpResponseMessage ValidationDTO(ItemDto entityDto)
        {
            var validationResult = ValidadorItemDto.Validate(entityDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new { Errors = errors })),
                    ReasonPhrase = "Failed Validation"
                };

                return response;
            }
            return null;
        }

        [AllowAnonymous]
        public IHttpActionResult Get(Guid id)
        {
            try
            {
                return Ok(Mapper.Map<Item, ItemDto>(ItemService.Get(id)));
            }
            catch (ArgumentNullException ex)
            {
                Log.Error("No se encontro el elemento con el valor indicado", ex);
                return BadRequest(ex.Message);
            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error("Error en el mapeo", ex);
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("No se encontro el elemento con el valor indicado", ex);
                return NotFound();
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        public IHttpActionResult GetAll()
        {
            try
            {
                var list = ItemService.GetAll().OrderByDescending(x => x.FECHA_CREACION).ToList();
                var rpta = Mapper.Map<IEnumerable<Item>,
                        IEnumerable<ItemDto>>(list);
                return Ok(rpta);

            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error("Error en el mapeo", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetPaged(PagedItem pagedItem)
        {
            try
            {
                var lista = ItemService.GetPaged(pagedItem.filtros, pagedItem.orden, pagedItem.startIndex,
                    pagedItem.length);

                return Ok(Mapper.Map<IEnumerable<Item>, IEnumerable<ItemDto>>(lista));
            }
            catch (ArgumentNullException ex)
            {
                Log.Error("No se acepta valor nulo", ex);
                return BadRequest(ex.Message);
            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error("Error en el mapeo", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        public IHttpActionResult CountAll(IList<FilterInfo> filters)
        {
            try
            {
                return Ok(ItemService.CountAll(filters));

            }
            catch (ArgumentNullException ex)
            {
                Log.Error("No se acepta valor nulo", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Insert(ItemDto entityDto)
        {
            try
            {
                var validationResult = ValidationDTO(entityDto);
                if(validationResult != null)
                {
                    return ResponseMessage(validationResult);
                }

                var entity = Mapper.Map<ItemDto, Item>(entityDto);
                var identity = (ClaimsIdentity)User.Identity;
                entity.AsNew(identity.Name);

                return Ok(Mapper.Map<Item, ItemDto>(
                    ItemService.Insert(entity)));

            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error("Error en el mapeo", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut]
        public IHttpActionResult Update(ItemDto entityDto)
        {
            try
            {
                var validationResult = ValidationDTO(entityDto);
                if (validationResult != null)
                {
                    return ResponseMessage(validationResult);
                }

                if (entityDto.FECHA_CREACION == 0)
                    throw new Exception ("FC Incorrecta");

                var entity = Mapper.Map<ItemDto, Item>(entityDto);
                var identity = (ClaimsIdentity)User.Identity;
                entity.AsUpdate(identity.Name);

                return Ok(Mapper.Map<Item, ItemDto>(
                    ItemService.UpdateAttach(entity)));

            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error("Error en el mapeo", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [AllowAnonymous]
        [HttpDelete]
        public IHttpActionResult Delete(ItemDto entityDto)
        {
            try
            {
                var entity = Mapper.Map<ItemDto, Item>(entityDto);
                var identity = (ClaimsIdentity)User.Identity;
                entity.AsDelete(identity.Name);

                return Ok(Mapper.Map<Item, ItemDto>(ItemService.DeleteEstado(entity)));
            }
            catch (ArgumentNullException ex)
            {
                Log.Error("No se acepta valor nulo", ex);
                return BadRequest(ex.Message);
            }
            catch (ValidationException ex)
            {
                Log.Error("Error de validacion", ex);
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("No se encontro el elemento con el valor indicado", ex);
                return NotFound();
            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error("Error en el mapeo", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetItemsByFilter(FilterInfo filtro)
        {
            try
            {
                var list = ItemService.GetAllByFilter(filtro);
                var rpta = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemDto>>
                    (list);

                return Ok(rpta);
            }
            catch (ArgumentNullException ex)
            {
                Log.Error("No se acepta valor nulo", ex);
                return BadRequest(ex.Message);
            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error("Error en el mapeo", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetListaItemByFiltro(FilterListaEntidad filtro)
        {
            try
            {
                filtro.tabla = Global.TItem();
                filtro.consulta = "PAGED";
                var lista = ProcedureService.GetAllStoreProcedureEntity
                    <Item, FilterListaEntidad>("str_GetListaEntidadByFiltro @tipo, @valor, @tabla, @consulta, @p_inicio, @p_intervalo", filtro);

                var rpta = Mapper.Map<IEnumerable<Item>, IEnumerable<ItemDto>>(lista);
                return Ok(rpta);
            }
            catch (ArgumentNullException ex)
            {
                Log.Error("No se acepta valor nulo", ex);
                return BadRequest(ex.Message);
            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error("Error en el mapeo", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult GetTotalItemByFiltro(FilterListaEntidad filtro)
        {
            try
            {
                filtro.tabla = Global.TItem();
                filtro.consulta = "TOTAL";
                var ltotal = ProcedureService.GetAllStoreProcedure
                    <Total, FilterListaEntidad>("str_GetListaEntidadByFiltro @tipo, @valor, @tabla, @consulta, @p_inicio, @p_intervalo", filtro);

                return Ok(ltotal.FirstOrDefault()?.total);
            }
            catch (ArgumentNullException ex)
            {
                Log.Error("No se acepta valor nulo", ex);
                return BadRequest(ex.Message);
            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error("Error en el mapeo", ex);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}