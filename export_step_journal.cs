using System;
using NXOpen;

public class NXJournal
{
    public static void Main(string[] args)
    {
        try
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;

            if (workPart != null)
            {
                string inputFilePath = workPart.FullPath; // Usa la ruta completa de la parte abierta como input

                string outputDirectory = @"C:\Users\Disegno-07\Desktop\costantini\"; // Definir la ubicación donde guardar (output)

                // Initialize a StepCreator object
                StepCreator stepCreator = theSession.DexManager.CreateStepCreator();

                stepCreator.ExportAs = StepCreator.ExportAsOption.Ap214;
                stepCreator.SettingsFile = @"C:\Siemens\NX2007\step214ug\ugstep214.def"; // Archivo de configuración
                stepCreator.EntityNames = StepCreator.EntityNameOption.ShortName;
                stepCreator.InputFile = inputFilePath;

                // Extraer el nombre de la parte y recortarlo para que sólo admita los primeros 5 caracteres
                string partName = workPart.Name;
                string slicedString = partName.Substring(0, 7);
                string outputFileName = slicedString + ".stp";
                string outputFilePath = System.IO.Path.Combine(outputDirectory, outputFileName);

                stepCreator.OutputFile = outputFilePath;
                stepCreator.FileSaveFlag = false;
                stepCreator.LayerMask = "1-256";
                stepCreator.ProcessHoldFlag = true;

                // Commit al archivo Step
                NXObject nXObject = stepCreator.Commit();

                // Destruye el objeto StepCreator al terminar
                stepCreator.Destroy();
            }
            else
            {
                
                Console.WriteLine("No se encontro una pieza activa");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocurrió un error: " + ex.Message);
        }
    }

    public static int GetUnloadOption(string dummy) { return (int)Session.LibraryUnloadOption.Immediately; }
}
