using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using DbEntidades.Entities;
using LibDB2;
using System.Collections;
using DbEntidades.Operators;

namespace DbEntidades
{
    public class Seguridad
    {
        public static int IdUsuarioActual = 0; //siempre mantiene en static el usuario actual

        public static bool Permiso(string permiso)
        {
            return true;
        }
    }
}