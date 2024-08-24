
namespace Core.Dominio
{
    using Core.Dominio.Entidades;
    using Core.Dominio.Specification;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;

    public class ServiceAbstractReporte<TEntity> : IServiceReporte<TEntity> 
        where TEntity : EntityReporte
    {
        #region Members

        private Dominio.IRepositoryReporte<TEntity> _repository;

        #endregion

        #region Constructor

        //public ServiceAbstract() { }
        public ServiceAbstractReporte(IRepositoryReporte<TEntity> repository)
        {
            this._repository = repository;
        }

        protected IRepositoryReporte<TEntity> getRepository()
        {
            return _repository;
        }

        #endregion

        #region funciones

        public virtual TEntity Get(Guid id)
        {
            if (id == null) throw new ArgumentNullException("Identificador null");
            return _repository.Get(id);
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            if (id == null) throw new ArgumentNullException("Identificador null");
            return await _repository.GetAsync(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return _repository.Find(match);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _repository.FindAsync(match);
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.FindAll(filter);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _repository.FindAllAsync(filter);
        }

        public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.GetFiltered(filter);
        }

        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _repository.GetFilteredAsync(filter);
        }

        public virtual IEnumerable<TEntity> GetFiltered(IEnumerable<Expression<Func<TEntity, bool>>> filters)
        {
            return _repository.GetFiltered(filters);
        }

        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync
            (IEnumerable<Expression<Func<TEntity, bool>>> filters)
        {
            return await _repository.GetFilteredAsync(filters);
        }

        public virtual IEnumerable<TEntity> GetAllByFilter(FilterInfo filter)
        {
            return _repository.GetAllByFilter(filter);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllByFilterAsync(FilterInfo filter)
        {
            return await _repository.GetAllByFilterAsync(filter);
        }

        public virtual IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification)
        {
            return _repository.AllMatching(specification);
        }

        public virtual async Task<IEnumerable<TEntity>> AllMatchingAsync
            (ISpecification<TEntity> specification)
        {
            return await _repository.AllMatchingAsync(specification);
        }

        public virtual IEnumerable<TEntity> GetPaged(IList<FilterInfo> filters, IList<OrderInfo> orders,
            int skip, int pageCount)
        {
            if (filters == null || orders == null) throw new ArgumentNullException("GetPaged");
            return _repository.GetPaged(filters, orders, skip, pageCount);
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(IList<FilterInfo> filters, IList<OrderInfo> orders,
            int skip, int pageCount)
        {
            if (filters == null || orders == null) throw new ArgumentNullException("GetPaged");
            return await _repository.GetPagedAsync(filters, orders, skip, pageCount);
        }

        public virtual int CountAll(IList<FilterInfo> filters)
        {
            if (filters == null) throw new ArgumentNullException("CountAll");
            return _repository.CountAll(filters);
        }

        public virtual async Task<int> CountAllAsync(IList<FilterInfo> filters)
        {
            if (filters == null) throw new ArgumentNullException("CountAll");
            return await _repository.CountAllAsync(filters);
        }

        public virtual int CountAll(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException("CountAll");
            return _repository.CountAll(filter);
        }

        public virtual async Task<int> CountAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException("CountAll");
            return await _repository.CountAllAsync(filter);
        }

        public virtual int CountAll()
        {
            return _repository.CountAll();
        }

        public virtual async Task<int> CountAllAsync()
        {
            return await _repository.CountAllAsync();
        }

        public virtual IEnumerable<TEntityStoreProcedure> 
            GetAllStoreProcedure<TEntityStoreProcedure, TEntityFilter>
            (string storeProcedureName, TEntityFilter parameters) 
            where TEntityStoreProcedure : EntityReporte 
            where TEntityFilter : FilterReporte
        {
            IList<object> parametersStoreProcedure = new List<object>();

            // Get property array
            var properties = parameters.GetType().GetProperties(); ;

            foreach (var p in properties)
            {
                string name = p.Name;
                var value = p.GetValue(parameters, null);

                var parameterStoreProcedure = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = name,
                    Value = value
                };

                parametersStoreProcedure.Add(parameterStoreProcedure);

            }
            
            var rpta = _repository.UnitOfWork.ExecuteQuery<TEntityStoreProcedure>(storeProcedureName,
                parametersStoreProcedure.ToArray());
            return rpta.ToList();
        }
       
        public virtual IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedureOracle<TEntityStoreProcedure, TEntityFilter>
            (string storeProcedureName, TEntityFilter parameters)
            where TEntityStoreProcedure : EntityReporte
            where TEntityFilter : FilterReporte
        {
            IList<object> parametersStoreProcedure = new List<object>();

            // Get property array
            var properties = parameters.GetType().GetProperties(); ;

            foreach (var p in properties)
            {
                string name = p.Name;
                var value = p.GetValue(parameters, null);

                var parameterStoreProcedure = new Oracle.ManagedDataAccess.Client.OracleParameter //System.Data.SqlClient.SqlParameter
                {
                    ParameterName = name,
                    Value = value
                };

                parametersStoreProcedure.Add(parameterStoreProcedure);

            }

            var rpta = _repository.UnitOfWork.ExecuteQuery<TEntityStoreProcedure>(storeProcedureName,
                parametersStoreProcedure.ToArray());
            return rpta.ToList();
        }

        public virtual IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedureOracle<TEntityStoreProcedure>(string storeProcedureName)
            where TEntityStoreProcedure : EntityReporte
        {
            IList<object> parametersStoreProcedure = new List<object>();
            var rpta = _repository.UnitOfWork.ExecuteQuery<TEntityStoreProcedure>(storeProcedureName,
                parametersStoreProcedure.ToArray());
            return rpta.ToList();
        }

        public virtual IEnumerable<TEntityStoreProcedure>
           GetAllStoreProcedureEntity<TEntityStoreProcedure, TEntityFilter>
           (string storeProcedureName, TEntityFilter parameters)
           where TEntityStoreProcedure : Entity
           where TEntityFilter : FilterReporte
        {
            IList<object> parametersStoreProcedure = new List<object>();

            try
            {
                // Get property array
                var properties = parameters.GetType().GetProperties(); ;

                foreach (var p in properties)
                {
                    string name = p.Name;
                    var value = p.GetValue(parameters, null);

                    var parameterStoreProcedure = new System.Data.SqlClient.SqlParameter
                    {
                        ParameterName = name,
                        Value = value
                    };

                    parametersStoreProcedure.Add(parameterStoreProcedure);

                }

                var rpta = _repository.UnitOfWork.ExecuteQuery<TEntityStoreProcedure>(storeProcedureName,
                    parametersStoreProcedure.ToArray());
                return rpta.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
        }

        public IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedure<TEntityStoreProcedure>(string storeProcedureName)
            where TEntityStoreProcedure : EntityReporte
        {
            IList<object> parametersStoreProcedure = new List<object>();
            var rpta = _repository.UnitOfWork.ExecuteQuery<TEntityStoreProcedure>(storeProcedureName,
                parametersStoreProcedure.ToArray());
            return rpta.ToList();
        }

        public IEnumerable<TEntityStoreProcedure>
           GetAllStoreProcedureEntity<TEntityStoreProcedure>(string storeProcedureName)
           where TEntityStoreProcedure : Entity
        {
            IList<object> parametersStoreProcedure = new List<object>();
            var rpta = _repository.UnitOfWork.ExecuteQuery<TEntityStoreProcedure>(storeProcedureName,
                parametersStoreProcedure.ToArray());
            return rpta.ToList();
        }

        public virtual int
            ExecuteStoredProcedure<TEntityFilter>
            (string storeProcedureName, TEntityFilter parameters)
            where TEntityFilter : FilterReporte
        {
            IList<object> parametersStoreProcedure = new List<object>();

            // Get property array
            var properties = parameters.GetType().GetProperties(); ;

            foreach (var p in properties)
            {
                string name = p.Name;
                var value = p.GetValue(parameters, null);

                var parameterStoreProcedure = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = name,
                    Value = value
                };

                parametersStoreProcedure.Add(parameterStoreProcedure);

            }

            return _repository.UnitOfWork.ExecuteCommand(storeProcedureName,
                parametersStoreProcedure.ToArray());
        }
        /*
        public byte[] GenerarExcel(String titulo, IEnumerable<String> header, IEnumerable<SegComItemReporte> body)
        {
            //ExcelPackage package = null;
            using (var package = new ExcelPackage())
            {

                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Reporte");

                worksheet.Cells[2, 1, 2, 8].Merge = true;
                worksheet.Cells[2, 1].Value = titulo;
                using (var range = worksheet.Cells[2, 1, 2, 8])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Font.Color.SetColor(Color.Gray);
                    range.Style.Font.Size = 20;
                }
                int colPosition = 1;
                foreach (String head in header)
                {
                    worksheet.Cells[4, colPosition].Value = head;
                    colPosition++;
                }
                using (var range = worksheet.Cells[4, 1, 4, colPosition - 1])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    Color black = Color.Black;
                    range.Style.Font.Color.SetColor(black);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                int rowPosition = 5;


                foreach (SegComItemReporte row in body)
                {
                    worksheet.Cells[rowPosition, 1].Value = row.numeroItem;
                    worksheet.Cells[rowPosition, 2].Value = row.descripcion;
                    worksheet.Cells[rowPosition, 3].Value = row.nombreProveedor;
                    worksheet.Cells[rowPosition, 4].Value = convertDate(row.fechaRecepcion);
                    worksheet.Cells[rowPosition, 5].Value = Math.Round(row.cantidadPO,2);
                    worksheet.Cells[rowPosition, 6].Value = Math.Round(row.cantidad,2);
                    worksheet.Cells[rowPosition, 7].Value = row.unidad;
                    worksheet.Cells[rowPosition, 8].Value = row.guias;
                    worksheet.Cells[rowPosition, 9].Value = row.guiasAntapaccay;
                    worksheet.Cells[rowPosition, 10].Value = row.guiaTransporte;
                    worksheet.Cells[rowPosition, 11].Value = row.transportista;
                    worksheet.Cells[rowPosition, 12].Value = row.nombreConductor;
                    worksheet.Cells[rowPosition, 13].Value = row.codigoVehiculo;
                    worksheet.Cells[rowPosition, 14].Value = row.placaRemolque;
                    worksheet.Cells[rowPosition, 15].Value = convertDate(row.salidaLima);
                    worksheet.Cells[rowPosition, 16].Value = (row.llegadaAqp == 0 && row.salidaLima != 0) ? "En tránsito" : convertDate(row.llegadaAqp);
                    worksheet.Cells[rowPosition, 17].Value = (row.salidaAqp == 0 && row.salidaLima != 0) ? "En tránsito" : convertDate(row.salidaAqp);
                    worksheet.Cells[rowPosition, 18].Value = (row.llegadaMina == 0 && row.fechaTentativaFin > 0) ? "Fecha Aproximada de Llegada a Mina " + convertDate(row.fechaTentativaFin) : convertDate(row.llegadaMina);

                    rowPosition++;
                }


                using (var range = worksheet.Cells[5, 1, rowPosition - 1, colPosition - 1])
                {
                    range.Style.Font.Bold = false;
                    range.Style.Font.Color.SetColor(Color.Black);
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }


                worksheet.View.PageLayoutView = false;
                package.Workbook.Properties.Title = "ReporteSeguimientoUsuario";
                package.Workbook.Properties.Company = "Innnovacis Inc.";

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                //var stream = new MemoryStream(package.GetAsByteArray());

                /*var xlFile = new FileInfo("file0002.xlsx");
                if (xlFile.Exists)
                {
                    xlFile.Delete();
                }
                package.SaveAs(xlFile);*/
                //package.Save();
     /*           return package.GetAsByteArray();

            }


        }
*/
        public String convertDate(long time)
        {
            if (time > 0)
            {
                return (new DateTime(1970, 1, 1)).AddMilliseconds(time).ToString("yyyy/MM/dd");
            }
            return "No registrada";
        }

        #endregion

    }
}
