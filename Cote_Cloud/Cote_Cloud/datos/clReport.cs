using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Drawing;
using System.Xml;
using System.Data;
using System.IO;


namespace Cote_Cloud.datos
{
    public class clReport
    {
        static DataTable tabla = new DataTable();
        public static string RprtMode = "Table";
        static int rows = 0;
        public void SetMode(string mode)
        {
            RprtMode = mode;
        }
        public string GetMode()
        {
            return RprtMode;
        }
        public void asignarT(DataTable aa)
        {
            tabla = aa;
        }
        public int returnRows()
        {
            return rows;
        }
        public void setEmptyRows()
        {
            rows = 0;
        }
        private string numbersInserted(string number)
        {
            string value = "";
            if (number == "0"||number=="")
            {
                value = "";
            }
            else
            {
                value = number;
            }
            return value;
        }
        #region Properties
        public DataSet RprtDS = new DataSet();
        public DataSet re()
        {
            return RprtDS;
        }
        #endregion
        public clReport()
        {
            DataSet ds = new DataSet("Nombre");
            ds.Tables.Add(tabla);
            RprtDS = ds;
            rows += tabla.Rows.Count;
            tabla = new DataTable();
        }
        #region Methods
         public string GetRprtStr(int fontsize = 12)
        {
            StringBuilder StrBuilder = new StringBuilder();
            StringWriter StrWriter = new StringWriter(StrBuilder);
            XmlWriterSettings RprtSetts = new XmlWriterSettings();
            RprtSetts.Indent = true;
            RprtSetts.IndentChars = "\t";
            RprtSetts.NewLineOnAttributes = true;
            XmlWriter Rprt = XmlWriter.Create(StrWriter, RprtSetts);
            DataTable DT = RprtDS.Tables[0];
            int columns = DT.Columns.Count;
            try
            {
                Rprt.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                Rprt.WriteStartElement("", "Report", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
                #region DataSource

                Rprt.WriteStartElement("DataSources");
                Rprt.WriteStartElement("DataSource");
                Rprt.WriteAttributeString("Name", RprtDS.DataSetName);
                Rprt.WriteStartElement("ConnectionProperties");
                Rprt.WriteElementString("DataProvider", "Oracle");
                Rprt.WriteElementString("ConnectString", "TeLaCreisteWe");
                Rprt.WriteElementString("IntegratedSecurity", "true");
                Rprt.WriteEndElement(); //ConnectionProperties
                Rprt.WriteEndElement(); //DataSource
                Rprt.WriteEndElement(); //DataSources
                Rprt.WriteStartElement("DataSets");
                Rprt.WriteStartElement("DataSet");
                Rprt.WriteAttributeString("Name", RprtDS.DataSetName);
                Rprt.WriteStartElement("Query");
                Rprt.WriteElementString("DataSourceName", RprtDS.DataSetName);
                Rprt.WriteElementString("CommandType", "Text");
                Rprt.WriteElementString("CommandText", "TeLaCreisteX2");
                Rprt.WriteElementString("Timeout", "10");
                Rprt.WriteEndElement();//Query
                Rprt.WriteStartElement("Fields");
                foreach (DataColumn dc in DT.Columns)
                {
                    Rprt.WriteStartElement("Field");
                    Rprt.WriteAttributeString("Name", dc.ColumnName);
                    Rprt.WriteElementString("DataField", dc.ColumnName);
                    Rprt.WriteEndElement(); //Field
                }
                Rprt.WriteEndElement(); //Fields
                Rprt.WriteEndElement(); //DataSet
                Rprt.WriteEndElement(); //DataSets

                #endregion
                double widnum = (DT.Columns.Count < 3) ? 3.5 * DT.Columns.Count : 7.5 / DT.Columns.Count;
                double leftmargin = (8.5 - (widnum * DT.Columns.Count)) / 2;
                Rprt.WriteStartElement("Body");
                Rprt.WriteStartElement("ReportItems");
                Rprt.WriteStartElement("Table");
                Rprt.WriteAttributeString("Name", DT.TableName);
                Rprt.WriteStartElement("Top");
                Rprt.WriteString("1cm");
                Rprt.WriteEndElement(); //Top
                Rprt.WriteStartElement("Left");
                Rprt.WriteString((leftmargin).ToString(System.Globalization.CultureInfo.InvariantCulture) + "in");
                Rprt.WriteEndElement(); //Top
                #region TableBody
                Rprt.WriteStartElement("Details");
                Rprt.WriteStartElement("TableRows");
                Rprt.WriteStartElement("TableRow");
                Rprt.WriteStartElement("TableCells");
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    Rprt.WriteStartElement("TableCell");
                    Rprt.WriteStartElement("ReportItems");
                    Rprt.WriteStartElement("Textbox");
                    Rprt.WriteAttributeString("Name", "Txt" + i);
                    Rprt.WriteStartElement("ZIndex");
                    Rprt.WriteString(i.ToString());
                    Rprt.WriteEndElement(); //ZIndex
                    Rprt.WriteStartElement("CanGrow");
                    Rprt.WriteString("true");
                    Rprt.WriteEndElement(); //CanGrow
                    Rprt.WriteStartElement("Style");

                    Rprt.WriteStartElement("BorderColor");
                    Rprt.WriteStartElement("Bottom");
                    Rprt.WriteString("Black");
                    Rprt.WriteEndElement(); //Bottom
                    Rprt.WriteStartElement("Right");
                    Rprt.WriteString("Black");
                    Rprt.WriteEndElement(); //Right
                    Rprt.WriteStartElement("Left");
                    Rprt.WriteString("Black");
                    Rprt.WriteEndElement(); //Left
                    Rprt.WriteEndElement(); //BorderColor

                    Rprt.WriteStartElement("BorderStyle");
                    Rprt.WriteStartElement("Bottom");
                    Rprt.WriteString("Solid");
                    Rprt.WriteEndElement(); //Bottom
                    Rprt.WriteStartElement("Right");
                    Rprt.WriteString("Solid");
                    Rprt.WriteEndElement(); //Right
                    Rprt.WriteStartElement("Left");
                    Rprt.WriteString("Solid");
                    Rprt.WriteEndElement(); //Left
                    Rprt.WriteEndElement(); //BorderStyle

                    Rprt.WriteStartElement("BorderWidth");
                    Rprt.WriteStartElement("Bottom");
                    Rprt.WriteString("1pt");
                    Rprt.WriteEndElement(); //Bottom
                    Rprt.WriteStartElement("Right");
                    Rprt.WriteString("1pt");
                    Rprt.WriteEndElement(); //Right
                    Rprt.WriteStartElement("Left");
                    Rprt.WriteString("1pt");
                    Rprt.WriteEndElement(); //Left
                    Rprt.WriteEndElement(); //BorderWidth

                    Rprt.WriteStartElement("TextAlign");
                    Rprt.WriteString("Center");
                    Rprt.WriteEndElement(); //TextAlign

                    Rprt.WriteStartElement("FontSize");
                    Rprt.WriteString(fontsize.ToString() + "pt");
                    Rprt.WriteEndElement(); //FontSize

                    Rprt.WriteStartElement("VerticalAlign");
                    Rprt.WriteString("Middle");
                    Rprt.WriteEndElement(); //VerticalAlign

                    Rprt.WriteEndElement(); //Style
                    Rprt.WriteStartElement("Value");
                    Rprt.WriteString("= Fields!" + DT.Columns[i].ColumnName + ".Value");
                    Rprt.WriteEndElement(); //Value
                    Rprt.WriteEndElement(); //TexBox
                    Rprt.WriteEndElement(); //ReportItemns
                    Rprt.WriteEndElement(); //TableCell
                }
                Rprt.WriteEndElement(); //TableCells
                Rprt.WriteStartElement("Height");
                Rprt.WriteString("0.4in");
                Rprt.WriteEndElement(); //Height
                Rprt.WriteEndElement(); //TableRow
                Rprt.WriteEndElement(); //TableRows
                Rprt.WriteEndElement(); //Details
                #endregion  
                #region TableHeader
                Rprt.WriteStartElement("Header");
                Rprt.WriteStartElement("TableRows");
                Rprt.WriteStartElement("TableRow");
                Rprt.WriteStartElement("TableCells");
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    Rprt.WriteStartElement("TableCell");
                    Rprt.WriteStartElement("ReportItems");
                    Rprt.WriteStartElement("Textbox");
                    Rprt.WriteAttributeString("Name", "Txt" + (i + DT.Columns.Count + 1));
                    Rprt.WriteStartElement("ZIndex");
                    Rprt.WriteString((DT.Columns.Count + i + 1).ToString());
                    Rprt.WriteEndElement(); //ZIndex
                    Rprt.WriteStartElement("CanGrow");
                    Rprt.WriteString("true");
                    Rprt.WriteEndElement(); //CanGrow
                    Rprt.WriteStartElement("Style");

                    Rprt.WriteStartElement("BorderColor");
                    Rprt.WriteStartElement("Right");
                    Rprt.WriteString("Black");
                    Rprt.WriteEndElement(); //Right
                    Rprt.WriteStartElement("Left");
                    Rprt.WriteString("Black");
                    Rprt.WriteEndElement(); //Left
                    Rprt.WriteStartElement("Bottom");
                    Rprt.WriteString("Black");
                    Rprt.WriteEndElement(); //Bottom
                    Rprt.WriteStartElement("Top");
                    Rprt.WriteString("Black");
                    Rprt.WriteEndElement(); //Top
                    Rprt.WriteEndElement(); //BorderColor

                    Rprt.WriteStartElement("BorderStyle");
                    Rprt.WriteStartElement("Right");
                    Rprt.WriteString("Solid");
                    Rprt.WriteEndElement(); //Right
                    Rprt.WriteStartElement("Left");
                    Rprt.WriteString("Solid");
                    Rprt.WriteEndElement(); //Left
                    Rprt.WriteStartElement("Bottom");
                    Rprt.WriteString("Solid");
                    Rprt.WriteEndElement(); //Bottom
                    Rprt.WriteStartElement("Top");
                    Rprt.WriteString("Solid");
                    Rprt.WriteEndElement(); //Top
                    Rprt.WriteEndElement(); //BorderStyle

                    Rprt.WriteStartElement("BorderWidth");
                    Rprt.WriteStartElement("Right");
                    Rprt.WriteString("1pt");
                    Rprt.WriteEndElement(); //Right
                    Rprt.WriteStartElement("Left");
                    Rprt.WriteString("1pt");
                    Rprt.WriteEndElement(); //Left
                    Rprt.WriteStartElement("Bottom");
                    Rprt.WriteString("1pt");
                    Rprt.WriteEndElement(); //Bottom
                    Rprt.WriteStartElement("Top");
                    Rprt.WriteString("1pt");
                    Rprt.WriteEndElement(); //Top
                    Rprt.WriteEndElement(); //BorderWidth

                    Rprt.WriteStartElement("TextAlign");
                    Rprt.WriteString("Center");
                    Rprt.WriteEndElement(); //TextAlign

                    Rprt.WriteStartElement("BackgroundColor");
                    Rprt.WriteString("LightGrey");
                    Rprt.WriteEndElement(); //BackgroundColor
                    Rprt.WriteStartElement("FontSize");
                    Rprt.WriteString(fontsize.ToString() + "pt");
                    Rprt.WriteEndElement(); //FontSize

                    Rprt.WriteStartElement("VerticalAlign");
                    Rprt.WriteString("Middle");
                    Rprt.WriteEndElement(); //VerticalAlign

                    Rprt.WriteEndElement(); //Style
                    Rprt.WriteStartElement("Value");
                    Rprt.WriteString(DT.Columns[i].ColumnName);
                    Rprt.WriteEndElement(); //Value
                    Rprt.WriteEndElement(); //TexBox
                    Rprt.WriteEndElement(); //ReportItemns
                    Rprt.WriteEndElement(); //TableCell
                }
                Rprt.WriteEndElement(); //TableCells
                Rprt.WriteStartElement("Height");
                Rprt.WriteString("0.4in");
                Rprt.WriteEndElement(); //Height
                Rprt.WriteEndElement(); //TableRow
                Rprt.WriteEndElement(); //TableRows
                Rprt.WriteEndElement(); //Details
                #endregion
                #region TableSettings
                Rprt.WriteStartElement("TableColumns");
                string wid = (widnum).ToString(System.Globalization.CultureInfo.InvariantCulture) + "in";
                foreach (DataColumn dc in DT.Columns)
                {
                    Rprt.WriteStartElement("TableColumn");
                    Rprt.WriteStartElement("Width");
                    Rprt.WriteString(wid);
                    Rprt.WriteEndElement(); //Width
                    Rprt.WriteEndElement(); //TableColumn
                }
                Rprt.WriteEndElement(); //TableColumns
                #endregion
                Rprt.WriteEndElement(); //Table
                Rprt.WriteEndElement(); //ReportItems
                Rprt.WriteStartElement("Height");
                Rprt.WriteString("11in");
                Rprt.WriteEndElement(); //Height
                Rprt.WriteEndElement(); //Body
                Rprt.WriteStartElement("Width");
                Rprt.WriteString("8.5in");
                Rprt.WriteEndElement(); //Width
                Rprt.WriteEndElement(); //Report End
                Rprt.Flush();
                Rprt.Close();
                return StrBuilder.ToString();
            }
            catch
            {
                return "Woops";
            }
        }
        public MemoryStream GetRprtStream(int fontsize = 12)
        {
            byte[] RprtBytes = Encoding.UTF8.GetBytes(GetRprtStr(fontsize));
            return new MemoryStream(RprtBytes);
        }
        public string GetSampleStr()
        {
            StringBuilder StrBuilder = new StringBuilder();
            StringWriter StrWriter = new StringWriter(StrBuilder);
            XmlWriterSettings RprtSetts = new XmlWriterSettings();
            RprtSetts.Indent = true;
            RprtSetts.IndentChars = "\t";
            RprtSetts.NewLineOnAttributes = true;
            XmlWriter Rprt = XmlWriter.Create(StrWriter, RprtSetts);
            Rprt.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
            Rprt.WriteStartElement("", "Report", "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition");
            Rprt.WriteStartElement("Body");
            Rprt.WriteStartElement("ReportItems");
            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "Txt1");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString("1");
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Paragraphs");
            Rprt.WriteStartElement("Paragraph");
            Rprt.WriteStartElement("TextRuns");
            Rprt.WriteStartElement("TextRun");
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Hallo");
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TextRun
            Rprt.WriteEndElement(); //TextRuns
            Rprt.WriteEndElement(); //Paragraph
            Rprt.WriteEndElement(); //Paragraphs
            Rprt.WriteStartElement("Style");

            Rprt.WriteEndElement(); //Style
            Rprt.WriteEndElement(); //TexBox
            Rprt.WriteEndElement(); //ReportItems
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement(); //Height
            Rprt.WriteEndElement(); //Body
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement(); //Width
            Rprt.WriteStartElement("Page");
            Rprt.WriteEndElement(); //Page
            Rprt.WriteEndElement(); //Report End
            Rprt.Flush();
            Rprt.Close();
            return StrBuilder.ToString();
        }
        public MemoryStream GetSampleStream()
        {
            byte[] RprtBytes = Encoding.UTF8.GetBytes(GetSampleStr());
            return new MemoryStream(RprtBytes);
        }
        private string GetFormStr()
        {
            StringBuilder StrBuilder = new StringBuilder();
            StringWriter StrWriter = new StringWriter(StrBuilder);
            XmlWriterSettings RprtSetts = new XmlWriterSettings();
            RprtSetts.Indent = true;
            RprtSetts.IndentChars = "\t";
            RprtSetts.NewLineOnAttributes = true;
            RprtSetts.ConformanceLevel = ConformanceLevel.Auto;
            XmlWriter Rprt = XmlWriter.Create(StrWriter, RprtSetts);
            DataTable DT = RprtDS.Tables[0];
            Rprt.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
            Rprt.WriteStartElement("", "Report", "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");

            Rprt.WriteStartElement("Body");
            Rprt.WriteStartElement("ReportItems");
            #region Title
            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtTitle");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("1in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("20pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("20pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Boleta de matrícula");
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox
            #endregion Title
            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtStudentDataLine");
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("0.63in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("7.5in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("3pt");
            Rprt.WriteEndElement();//Height
            Rprt.WriteStartElement("Style");
            Rprt.WriteStartElement("BorderStyle");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("Solid");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderStyle
            Rprt.WriteStartElement("BorderColor");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("Black");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderColor
            Rprt.WriteStartElement("BorderWidth");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("1.3pt");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderColor
            Rprt.WriteEndElement();//Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString(" ");
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement();//TextBox

            #region StudentData
            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtStudentData");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("0.8in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("6in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("20pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Left");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Datos del estudiante");
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtName");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("1.3in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Nombre:\n" + DT.Rows[0][1].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtLastNames");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("1.3in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Apellidos: \n" + DT.Rows[0][2].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtID");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("1.3in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("N° Cédula: \n" + DT.Rows[0][0].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtTel");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("1.7in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Teléfono:\n" + DT.Rows[0][7].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtMail");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("1.7in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Correo Electrónico: \n" + DT.Rows[0][3].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtFolCode");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("1.7in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("N° consecutivo: \n" + DT.Rows[0][40].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtBDate");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("2.2in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            DateTime date = Convert.ToDateTime(DT.Rows[0][6].ToString());
            Rprt.WriteString("Fecha de Nacimiento:\n" + date.ToShortDateString().ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtAge");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("2.2in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Edad: \n" + DT.Rows[0][10].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtSex");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("2.2in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Sexo: \n" + DT.Rows[0][11].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtOption1");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("2.7in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3.75in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Opción 1: \n" + DT.Rows[0][4].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtOption2");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("4.25in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("2.7in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3.75in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Opción 2: \n" + numbersInserted(DT.Rows[0][5].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtNation");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("3.2in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3.75in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Nacionalidad: \n" + DT.Rows[0][8].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtSchool");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("4.25in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("3.2in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3.75in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Colegio de Procedencia: \n" + DT.Rows[0][9].ToString());
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox
            #endregion StudentData

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtFatherDataLine");
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("3.23in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("7.5in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("3pt");
            Rprt.WriteEndElement();//Height
            Rprt.WriteStartElement("Style");
            Rprt.WriteStartElement("BorderStyle");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("Solid");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderStyle
            Rprt.WriteStartElement("BorderColor");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("Black");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderColor
            Rprt.WriteStartElement("BorderWidth");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("1.3pt");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderColor
            Rprt.WriteEndElement();//Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString(" ");
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement();//TextBox

            #region Father

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtFatherData");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("3.5in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("6in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("20pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Left");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Datos del padre");
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtFatherCompName");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("4in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Nombre Completo:\n" + numbersInserted(DT.Rows[0][17].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtFatherID");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("4in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("N° de Cédula: \n" + numbersInserted(DT.Rows[0][20].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtFatherTel");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("4in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Teléfono: \n" + numbersInserted(DT.Rows[0][23].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtFatherIncome");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("4.5in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Ingreso Mensual:\n" + numbersInserted(DT.Rows[0][28].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtFatherWork");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("4.5in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Ocupación: \n" + numbersInserted(DT.Rows[0][31].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtFatherWorkTel");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("4.5in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Tel. Trabajo: \n" + numbersInserted(DT.Rows[0][34].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            #endregion

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtMotherDataLine");
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("4.6in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("7.5in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("3pt");
            Rprt.WriteEndElement();//Height
            Rprt.WriteStartElement("Style");
            Rprt.WriteStartElement("BorderStyle");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("Solid");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderStyle
            Rprt.WriteStartElement("BorderColor");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("Black");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderColor
            Rprt.WriteStartElement("BorderWidth");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("1.3pt");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderColor
            Rprt.WriteEndElement();//Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString(" ");
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement();//TextBox

            #region Mother

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtMotherData");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("4.8in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("6in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("20pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Left");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Datos de la madre");
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtMotherCompName");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("5.3in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Nombre Completo:\n" + numbersInserted(DT.Rows[0][18].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtMotherID");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("5.3in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("N° de Cédula: \n" + numbersInserted(DT.Rows[0][21].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtMotherTel");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("5.3in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Teléfono: \n" + numbersInserted(DT.Rows[0][24].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtMotherIncome");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("5.8in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Ingreso Mensual:\n" + numbersInserted(DT.Rows[0][27].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtMotherWork");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("5.8in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Ocupación: \n" + numbersInserted(DT.Rows[0][30].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtMotherWorkTel");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("5.8in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Tel. Trabajo: \n" + numbersInserted(DT.Rows[0][33].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox
            #endregion
            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtOtherDataLine");
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("5.83in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("7.5in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("3pt");
            Rprt.WriteEndElement();//Height
            Rprt.WriteStartElement("Style");
            Rprt.WriteStartElement("BorderStyle");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("Solid");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderStyle
            Rprt.WriteStartElement("BorderColor");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("Black");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderColor
            Rprt.WriteStartElement("BorderWidth");
            Rprt.WriteStartElement("Bottom");
            Rprt.WriteString("1.3pt");
            Rprt.WriteEndElement();
            Rprt.WriteEndElement();//BorderColor
            Rprt.WriteEndElement();//Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString(" ");
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement();//TextBox

            #region Other

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtOtherData");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("6.3in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("6in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("20pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Left");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Datos del encargado");
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtOtherCompName");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("6.6in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Nombre Completo:\n" + numbersInserted(DT.Rows[0][19].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtOtherID");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("6.6in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("N° de Cédula: \n" + numbersInserted(DT.Rows[0][22].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtOtherTel");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("6.6in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Teléfono: \n" + numbersInserted(DT.Rows[0][25].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtOtherIncome");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("0.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("7.1in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Ingreso Mensual:\n" + numbersInserted(DT.Rows[0][26].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox

            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtOtherWork");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("7.1in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("3in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Ocupación: \n" + numbersInserted(DT.Rows[0][29].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            Rprt.WriteStartElement("Textbox");
            Rprt.WriteAttributeString("Name", "TxtOtherWorkTel");
            Rprt.WriteStartElement("ZIndex");
            Rprt.WriteString((8).ToString());
            Rprt.WriteEndElement(); //ZIndex
            Rprt.WriteStartElement("CanGrow");
            Rprt.WriteString("true");
            Rprt.WriteEndElement(); //CanGrow
            Rprt.WriteStartElement("Left");
            Rprt.WriteString("6.5in");
            Rprt.WriteEndElement();//Left
            Rprt.WriteStartElement("Top");
            Rprt.WriteString("7.1in");
            Rprt.WriteEndElement();//Top
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("2in");
            Rprt.WriteEndElement();//Width
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("16pt");
            Rprt.WriteEndElement();//Height
            #region Style
            Rprt.WriteStartElement("Style");

            Rprt.WriteStartElement("TextAlign");
            Rprt.WriteString("Center");
            Rprt.WriteEndElement(); //TextAlign

            Rprt.WriteStartElement("FontSize");
            Rprt.WriteString("14pt");
            Rprt.WriteEndElement(); //FontSize

            Rprt.WriteStartElement("VerticalAlign");
            Rprt.WriteString("Middle");
            Rprt.WriteEndElement(); //VerticalAlign

            Rprt.WriteEndElement(); //Style
            #endregion Style
            Rprt.WriteStartElement("Value");
            Rprt.WriteString("Tel. Trabajo: \n" + numbersInserted(DT.Rows[0][32].ToString()));
            Rprt.WriteEndElement(); //Value
            Rprt.WriteEndElement(); //TexBox


            #endregion

            Rprt.WriteEndElement(); //ReportItems
            Rprt.WriteStartElement("Height");
            Rprt.WriteString("11in");
            Rprt.WriteEndElement(); //Height
            Rprt.WriteEndElement(); //Body
            Rprt.WriteStartElement("Width");
            Rprt.WriteString("8.5in");
            Rprt.WriteEndElement(); //Width
            Rprt.WriteEndElement(); //Report End
            Rprt.Flush();
            Rprt.Close();
            return StrBuilder.ToString();
        }
        public MemoryStream GetFormStream()
        {
            byte[] RprtBytes = Encoding.UTF8.GetBytes(GetFormStr());
            return new MemoryStream(RprtBytes);
        }
        public byte[] getBytes()
        {
            byte[] RprtBytes = Encoding.UTF8.GetBytes(GetFormStr());
            return RprtBytes;
        }
        #endregion
    }
}