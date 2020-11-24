using ControlCSA.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace ControlCSA.WS
{
    public class WsClient
    {
        public string Url_Agendaris = "http://190.13.186.53:8081/ws_clinica/WsObtenerAgendaRut.asmx";
        public string Url_AgendaClinicloud = "http://agenda.sanatorioaleman.cl/PideHoraAppWs/Movil/WSDatosPacientes.asmx";
        public string Url_AgendaClinicloud_QA = "https://190.200.100.14:90/PideHoraAppWs_qa/Movil/WSDatosPacientes.asmx/ReservaHoraMisHoras";
        public List<AgendaRis> obtenerAgendaRis(string Rut)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml("<soap:Envelope xmlns:soap='http://www.w3.org/2003/05/soap-envelope' xmlns:tem='http://tempuri.org/'><soap:Header/><soap:Body><tem:obtenerAgendaRut><tem:Rut>"+Rut+ "</tem:Rut></tem:obtenerAgendaRut></soap:Body></soap:Envelope> ");

                // create the request to your URL
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url_Agendaris);

                // add the headers
                // the SOAPACtion determines what action the web service should use
                // YOU MUST KNOW THIS and SET IT HERE
                request.Headers.Add("SOAPAction", "http://tempuri.org/obtenerAgendaRut");

                // set the request type
                // we user utf-8 but set the content type here

                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                // add our body to the request
                Stream stream = request.GetRequestStream();
                doc.Save(stream);
                stream.Close();

                // get the response back
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var objText = reader.ReadToEnd();

                        XmlDocument respuestaXml = new XmlDocument();
                        respuestaXml.LoadXml(objText);
                        XmlElement root = respuestaXml.DocumentElement;
                        string json = root.InnerText;

                        List<AgendaRis> listaServicios = JsonConvert.DeserializeObject<List<AgendaRis>>(json);

                        return listaServicios;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public List<ReservaClini> ObtenerAgendaClinicloud(string Rut, string Fecha_ini, string Fecha_fin)
        {
            try
            {

                XmlDocument doc = new XmlDocument();
                string strSoapEnvelope = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:tem='http://tempuri.org/\'><soapenv:Header/><soapenv:Body><tem:ReservaHoraMisHoras>";
                strSoapEnvelope += "<tem:vPAC_RUT>" + Rut + "</tem:vPAC_RUT>";
                strSoapEnvelope += "<tem:vPAC_PASAPORTE></tem:vPAC_PASAPORTE>";
                if (Fecha_ini == "") { strSoapEnvelope += "<tem:vFECHA_INI>''</tem:vFECHA_INI>"; }
                else { strSoapEnvelope += "<tem:vFECHA_INI>" + Fecha_ini + "</tem:vFECHA_INI>"; }
                if (Fecha_fin == "") { strSoapEnvelope += "<tem:vFECHA_FIN>''</tem:vFECHA_FIN>"; }
                else { strSoapEnvelope += "<tem:vFECHA_FIN>" + Fecha_fin + "</tem:vFECHA_FIN>"; }
                strSoapEnvelope += "<tem:vCLI_ID></tem:vCLI_ID>";
                strSoapEnvelope += "</tem:ReservaHoraMisHoras>";
                strSoapEnvelope += "</soapenv:Body></soapenv:Envelope>";
                //Crea XML
                doc.LoadXml(strSoapEnvelope);
                // create the request to your URL 
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url_AgendaClinicloud);
                request.Headers.Add("SOAPAction", "http://tempuri.org/ReservaHoraMisHoras");
                // set the request type
                // we user utf-8 but set the content type here
                request.Timeout = 10 * 1000;
                request.ContentType = "text/xml;charset=utf-8";
                request.Accept = "application/json";
                request.Method = "POST";

                // add our body to the request
                Stream stream = request.GetRequestStream();
                doc.Save(stream);
                stream.Close();

                // get the response back
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var objText = reader.ReadToEnd();
                        string json = objText.Substring(1, objText.IndexOf(";") - 2);
                        var reservas = JObject.Parse(json)["reservas"];
                        //var resultado = objText[0]["numero"].ToString();
                        //XmlDocument respuestaXml = new XmlDocument();
                        //respuestaXml.LoadXml(objText);
                        //XmlElement root = respuestaXml.DocumentElement;
                        //string json = root.InnerText;

                        List<ReservaClini> listaServicios = JsonConvert.DeserializeObject<List<ReservaClini>>(reservas.ToString());

                        return listaServicios;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
