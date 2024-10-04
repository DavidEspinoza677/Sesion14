using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        

        // Evento del botón para mostrar y guardar los datos
        private void btnMostrarGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Capturar los datos de los TextBox
                string nombre = txtNombre.Text;
                int edad = int.Parse(txtEdad.Text);
                int nota = int.Parse(txtNota.Text);
                char genero = char.Parse(txtGenero.Text);

                // Mostrar los datos en el ListBox
                lstDatos.Items.Add($"Nombre: {nombre}, Edad: {edad}, Nota: {nota}, Género: {genero}");

                // Guardar los datos en un archivo binario
                using (FileStream archivo = new FileStream("datos.dat", FileMode.Append, FileAccess.Write))
                using (BinaryWriter escritor = new BinaryWriter(archivo))
                {
                    escritor.Write(nombre.Length); // Escribir la longitud del nombre
                    escritor.Write(nombre.ToCharArray()); // Escribir el nombre
                    escritor.Write(edad); // Escribir la edad
                    escritor.Write(nota); // Escribir la nota
                    escritor.Write(genero); // Escribir el género
                }

                // Limpiar los TextBox después de agregar y guardar los datos
                txtNombre.Clear();
                txtEdad.Clear();
                txtNota.Clear();
                txtGenero.Clear();

                MessageBox.Show("Datos guardados correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Evento del botón para cargar y mostrar los datos desde el archivo binario
        private void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                lstDatos.Items.Clear(); // Limpiar el ListBox antes de cargar nuevos datos

                // Leer los datos desde el archivo binario
                using (FileStream archivo = new FileStream("datos.dat", FileMode.Open, FileAccess.Read))
                using (BinaryReader lector = new BinaryReader(archivo))
                {
                    while (archivo.Position != archivo.Length)
                    {
                        int longitudNombre = lector.ReadInt32(); // Leer la longitud del nombre
                        char[] nombreArray = lector.ReadChars(longitudNombre); // Leer el nombre
                        string nombre = new string(nombreArray); // Convertir a string
                        int edad = lector.ReadInt32(); // Leer la edad
                        int nota = lector.ReadInt32(); // Leer la nota
                        char genero = lector.ReadChar(); // Leer el género

                        // Mostrar los datos en el ListBox
                        lstDatos.Items.Add($"Nombre: {nombre}, Edad: {edad}, Nota: {nota}, Género: {genero}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}