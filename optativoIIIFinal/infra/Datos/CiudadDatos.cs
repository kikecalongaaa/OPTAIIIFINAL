﻿using System;
using System.Data;
using Infra.Conexion;
using Infra.Modelos;

namespace Infra.Datos
{
    public class CiudadDatos
    {
        private ConexionDB conexion;

        public CiudadDatos(string cadenaConexion)
        {
            conexion = new ConexionDB(cadenaConexion); //abrimos la conexion a la bd
        }

        public void insertarCiudad(CiudadModel ciudad)
        {
            var conn = conexion.GetConexion();//realizamos un Insert dentro de las columnas
            var comando = new Npgsql.NpgsqlCommand("INSERT INTO ciudad(id_ciudad, ciudad, depa, pc)" +
                                                "VALUES(@idXiudad, @ciudad, @depa, @pc)", conn);
            comando.Parameters.AddWithValue("idCiudad", ciudad.idCiudad);
            comando.Parameters.AddWithValue("ciudad", ciudad.ciudad);
            comando.Parameters.AddWithValue("depto", ciudad.depa);
            comando.Parameters.AddWithValue("cp", ciudad.pc);
            comando.ExecuteNonQuery();
        }

        public void modificarCiudad(CiudadModel ciudad)
        {
            var conn = conexion.GetConexion();//Update para los campos de ciudad y depa donde el idciudad sea igual al solicitado
            var comando = new Npgsql.NpgsqlCommand($"UPDATE ciudad SET ciudad = '{ciudad.ciudad}', depa = '{ciudad.depa}', " +
                                                   $"pc = {ciudad.pc}" +
                                                   $" WHERE idCiudad = {ciudad.idCiudad}", conn);

            comando.ExecuteNonQuery();
        }

        public void eliminarCiudad(int id)
        {
            var conn = conexion.GetConexion();//Eliminacion de datos de ciudad donde el idciudad sea igual al solicitado
            var comando = new Npgsql.NpgsqlCommand($"DELETE FROM ciudad" +
                                                $" WHERE idCiudad = {id}", conn);
            comando.ExecuteNonQuery();
        }
        public CiudadModel obtenerCiudadPorId(int id)
        {
            var conn = conexion.GetConexion();//Listamos todos los datos de La tabla ciudad donde sea igual al idciudad

            var comando = new Npgsql.NpgsqlCommand($"Select * from ciudad where idCiudad = {id}", conn);
            using var reader = comando.ExecuteReader();
            if (reader.Read())
            {
                return new CiudadModel
                {
                    idCiudad = reader.GetInt32("id_ciudad"),
                    ciudad = reader.GetString("ciudad"),
                    depa = reader.GetString("depto"),
                    pc = reader.GetInt32("cp")
                };
            }
            return null;
        }

        public List<CiudadModel> obtenerCiudades()//Listamos todo de la tabla de ciudad
        {
            var conn = conexion.GetConexion();
            var comando = new Npgsql.NpgsqlCommand($"Select * from ciudad", conn);
            using var reader = comando.ExecuteReader();
            List<CiudadModel> ciudades = new List<CiudadModel>();

            while (reader.Read())
            {
                var ciudad = new CiudadModel
                {
                    idCiudad = reader.GetInt32("id_ciudad"),
                    ciudad = reader.GetString("ciudad"),
                    depa = reader.GetString("depto"),
                    pc = reader.GetInt32("cp")
                };
                ciudades.Add(ciudad);
            }
            return ciudades;
        }
    }
}

