using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace contenido
{
    internal static class GestionUsuarios
    {
        // El archivo se auto-genera en bin/Debug/net8.0/usuarios.json
        private const string path = "usuarios.json";

        public static void GuardarNuevoUsuario(Usuario nuevoUsuario)
        {
            try
            {
                Biblioteca bib = new Biblioteca();

                // PASO 1: Si el archivo físico ya existe, levantamos los usuarios guardados
                if (File.Exists(path))
                {
                    string jsonExistente = File.ReadAllText(path);

                    // Deserializamos el texto plano a objetos en RAM
                    Biblioteca bibCargada = JsonSerializer.Deserialize<Biblioteca>(jsonExistente);

                    if (bibCargada != null && bibCargada.usuarios != null)
                    {
                        bib.usuarios = bibCargada.usuarios;
                    }
                }

                // PASO 2: Agregamos el nuevo registro usando el método de instancia de Biblioteca
                bib.agregar_usuario(nuevoUsuario);

                // PASO 3: Convertimos la estructura de RAM a JSON formateado (WriteIndented)
                JsonSerializerOptions opciones = new JsonSerializerOptions { WriteIndented = true };
                string convertidoAJson = JsonSerializer.Serialize(bib, opciones);

                // PASO 4: Escritura en disco
                File.WriteAllText(path, convertidoAJson);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[SISTEMA] ¡Usuario guardado exitosamente en usuarios.json!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[ERROR PERSISTENCIA] No se pudo escribir el archivo JSON: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
