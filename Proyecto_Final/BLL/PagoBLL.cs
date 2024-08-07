using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final.Models;
using Proyecto_Final.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


#nullable disable // Para quitar el aviso de nulls

namespace Proyecto_Final.BLL // BLL Para el metodo de pago
{
public class PagoBLL
    {
        private readonly ApplicationDbContext contexto;

        public PagoBLL(ApplicationDbContext _contexto)
        {
            contexto = _contexto;
        }

        public async Task<Pago> Buscar(int id)
        {
            Pago pago = new Pago();

            try
            {
                pago = await contexto.Pago.FindAsync(id);
            }
            catch (Exception)
            {
                throw;
            }
            return pago;
        }

        public List<Pago> GetList(Expression<Func<Pago, bool>> pago)
        {
            List<Pago> Lista = new List<Pago>();
            try
            {
                Lista = contexto.Pago
                .Where(p => p.Estado == true)
                .Where(pago)
                .AsNoTracking()
                .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return Lista;
        }

        public async Task CrearPago(Pago pago)
        {
            try
            {
                contexto.Pago.Add(pago);
                await contexto.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ActualizarPago(Pago pago)
        {
            try
            {
                contexto.Entry(pago).State = EntityState.Modified;
                await contexto.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EliminarPago(int id)
        {
            try
            {
                var pago = await contexto.Pago.FindAsync(id);
                if (pago != null)
                {
                    contexto.Pago.Remove(pago);
                    await contexto.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}