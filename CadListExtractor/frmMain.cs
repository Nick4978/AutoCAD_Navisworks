using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Controls;

namespace CadListExtractor
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            // Initialize the Navisworks application
            Autodesk.Navisworks.Api.Controls.ApplicationControl.ApplicationType = ApplicationType.SingleDocument;
            ApplicationControl.Initialize();
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            // current document

            // Check if there is an active document
            if (Autodesk.Navisworks.Api.Application.ActiveDocument == null)
            {
                // If no active document, prompt the user to open a document
                DocumentControl documentCtrl = new DocumentControl();

                // Set the document control as the main document control
                documentCtrl.SetAsMainDocument();

                // Open a file dialog to select a Navisworks document
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "NWD files|*.nwd";
                dlg.Title = "Open Navisworks Document";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Open the selected document
                    documentCtrl.Document.OpenFile(dlg.FileName);

                    // Extract properties from the opened document
                    ExtractProperties(documentCtrl.Document);
                }
            }
        }


        /// <summary>
        /// Extracts specific properties from the selected items in the provided Navisworks document.
        /// </summary>
        /// <remarks>This method selects all items in the document and iterates through their models to
        /// extract  properties from the "AutoCAD" property category, specifically the "Line Number" property.  The
        /// extracted property values are displayed in a text box and a message box is shown upon completion.</remarks>
        /// <param name="doc">The <see cref="Autodesk.Navisworks.Api.Document"/> instance representing the Navisworks document  from which
        /// properties will be extracted. This parameter cannot be null.</param>
        public void ExtractProperties(Autodesk.Navisworks.Api.Document doc)
        {

            // Select all items in the document
            doc.CurrentSelection.SelectAll();

            var ret = new List<string>();

            // 2. Iterate through selected items
            foreach (var model in doc.Models)
            {
                ;
                // 3. Get properties of each item

                // Iterate through all items in the model
                foreach (ModelItem item in model.RootItem.DescendantsAndSelf)
                {
                    // Check if the item has properties and iterate through them
                    foreach (PropertyCategory category in item.PropertyCategories.Where(x => x.DisplayName == "AutoCAD"))
                    {
                        // 4. Extract the category name if you want to use it later
                        string categoryName = category.DisplayName;

                        // 5. Iterate through properties in the category
                        foreach (var propData in category.Properties.Where(x => x.DisplayName == "Line Number").OrderBy(x => x.DisplayName).Select(x => x.Value.ToString()).Distinct())
                        {
                            // 4. Extract the property data

                            // Add the property data to the list
                            ret.Add(propData.Replace("DisplayString:", "").Trim());
                        }
                    }

                    // Example: Write to a CSV file
                    // File.AppendAllText("output.csv", $"{categoryName}, {propertyName}, {propertyValue}\n");

                    // Example: Print to the console
                    //Console.WriteLine($"{categoryName}, {propertyName}, {propertyValue}");

                }
            }

            // 5. Display the results
            
            // Write the results to the text box 
            txtResults.Text = string.Join(Environment.NewLine, ret.OrderBy(x => x).Distinct());

            // Display a message box to indicate completion
            MessageBox.Show("Extraction complete. Check the results in the text box.", "Extraction Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
