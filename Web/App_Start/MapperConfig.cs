
namespace WebMain.App_Start
{
    using AutoMapper;
    using Tareo.Aplicacion.Dto;
    using Tareo.Dominio.Entidades;
    using System;

    public class MapperConfig
    {
        public static void RegisterMapper()
        {
            // Mappes de la aplicacion

            Mapper.Initialize(cfg =>
            {
                // Mapas para convertir los String a Guid y viceversa
                cfg.CreateMap<string, Guid>().ConvertUsing((s) =>
                {
                    if (s == null || s.Length <= 0)
                        return Guid.Empty;
                    return new Guid(s);
                });
                cfg.CreateMap<Guid, string>().ConvertUsing((s) =>
                {
                    if (s == null || s == new Guid("00000000-0000-0000-0000-000000000000") || s == Guid.Empty)
                        return string.Empty;
                    return Convert.ToString(s);
                });

                //Mappas para convertir arreglos de bytes a String y viceversa (RowVersion)
                cfg.CreateMap<string, Byte[]>()
                .ConvertUsing((s) =>
                {
                    if (s == null)
                        s = string.Empty;
                    int NumberChars = s.Length;
                    byte[] bytes = new byte[NumberChars / 2];//9
                    for (int i = 0; i < NumberChars; i += 2)
                        bytes[i / 2] = Convert.ToByte(s.Substring(i, 2), 16);
                    return bytes;
                });
                cfg.CreateMap<Byte[], string>()
                .ConvertUsing((s) =>
                {
                    System.Text.StringBuilder hex = new System.Text.StringBuilder(s.Length * 2);
                    foreach (byte b in s)
                        hex.AppendFormat("{0:x2}", b);
                    return hex.ToString();
                });

                /**
                * Otros Mapeadores
                */

                
                /// Mapeadores para el modulo Prioridad
                cfg.CreateMap<Item, ItemDto>()
                    .IgnoreAllPropertiesWithAnInaccessibleSetter()
                    .IgnoreAllSourcePropertiesWithAnInaccessibleSetter()
                    .ForAllMembers(opt => opt.Condition(src => src != null));

                cfg.CreateMap<ItemDto, Item>()
                    .IgnoreAllPropertiesWithAnInaccessibleSetter()
                    .IgnoreAllSourcePropertiesWithAnInaccessibleSetter()
                    .ForAllMembers(opt => opt.Condition(src => src != null));

             
            });
        }
    }
}