using SevenSuite.BLL.Enums;
using SevenSuite.Entities;
using SeventSuite.DAL;
using System;
using System.Collections.Generic;

namespace SevenSuite.BLL
{
    public class ClienteService
    {
        private readonly ClienteDAL _clienteDal;
        private readonly CatalogoDAL _catalogoDal;

        public ClienteService()
        {
            _clienteDal = new ClienteDAL();
            _catalogoDal = new CatalogoDAL();
        }

        public int Save(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente));

            if (string.IsNullOrWhiteSpace(cliente.Cedula))
                throw new Exception(
                    ClienteErrorMessages.GetMessage(ClienteError.CedulaRequerida));

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new Exception(
                    ClienteErrorMessages.GetMessage(ClienteError.NombreRequerido));

            if (cliente.FechaNac == DateTime.MinValue)
                throw new Exception(
                    ClienteErrorMessages.GetMessage(ClienteError.FechaNacimientoRequerida));

            if (cliente.EstadoCivilId <= 0)
                throw new Exception(
                    ClienteErrorMessages.GetMessage(ClienteError.EstadoCivilRequerido));

            return _clienteDal.Upsert(cliente);
        }


        public void Delete(int id)
        {
            if (id <= 0)
                throw new Exception(
                    ClienteErrorMessages.GetMessage(ClienteError.IdInvalido));

            _clienteDal.Delete(id);
        }


        public Cliente GetById(int id)
        {
            if (id <= 0)
                return null;

            return _clienteDal.GetById(id);
        }

        public List<Cliente> Search(string cedula, string nombre)
        {
            return _clienteDal.Search(cedula, nombre);
        }

        public List<EstadoCivil> GetEstadosCiviles()
        {
            return _catalogoDal.GetEstadosCiviles();
        }
    }
}
