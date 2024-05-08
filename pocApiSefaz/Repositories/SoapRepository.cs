using pocApiSefaz.Repositories.Interfaces;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace pocApiSefaz.Repositories
{

    public class SoapRepository : ISoapRepository
    {
        private readonly IRabbitMQRepository _rabbitMQClient;

        public SoapRepository(IRabbitMQRepository rabbitMQClient)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            _rabbitMQClient = rabbitMQClient;
        }

        public IResult execute()
        {
            string messageToRabbit = "TESTE";

            //_rabbitMQClient.sendMessage(messageToRabbit);

            CallWebService();

            return TypedResults.NoContent();
        }

        private void CallWebService()
        {
            try
            {
                var _url = "https://homologacao.nfe.fazenda.sp.gov.br/ws/nfeautorizacao4.asmx";
                var _action = "http://xxxxxxxx/Service1.asmx?op=HelloWorld";

                XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
                X509Certificate2Collection certificates = CreateCertificates();
                HttpWebRequest webRequest = CreateWebRequest(_url, _action, certificates);
                InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                // do something usefull here like update your UI.
                asyncResult.AsyncWaitHandle.WaitOne();

                // get the response from the completed web request.
                string soapResult;
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                    Console.Write(soapResult);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                throw;
            }
        }

        private X509Certificate2Collection CreateCertificates()
        {
            string certName = @"C:\temp\cert.pfx";
            string password = @"rave23";

            X509Certificate2Collection certificates = new X509Certificate2Collection();

            certificates.Import(certName, password, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);

            return certificates;
        }

        private HttpWebRequest CreateWebRequest(string url, string action, X509Certificate2Collection certificates)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            //webRequest.Headers.Add("SOAPAction", action);
            webRequest.ClientCertificates = certificates;
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            return webRequest;
        }

        private XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(
                @"
                    <soap:Envelope 
                        xmlns:soap=""http://www.w3.org/2003/05/soap-envelope""
                        xmlns:nfe=""http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4"">
                        <soap:Header />
                        <soap:Body>
                            <nfe:nfeDadosMsg>
                                <infNFSe Id=""NFS35485002247896326000102000000000001124056512582822"">
                                    <xLocEmi>Santos</xLocEmi>
                                    <xLocPrestacao>Santos</xLocPrestacao>
                                    <nNFSe>11</nNFSe>
                                    <cLocIncid>3548500</cLocIncid>
                                    <xLocIncid>Santos</xLocIncid>
                                    <xTribNac>Assistência técnica.</xTribNac>
                                    <verAplic>EmissorWeb_1.2.0.3</verAplic>
                                    <ambGer>2</ambGer>
                                    <tpEmis>1</tpEmis>
                                    <procEmi>2</procEmi>
                                    <cStat>107</cStat>
                                    <dhProc>2024-05-03T15:27:36-03:00</dhProc>
                                    <nDFSe>126553</nDFSe>
                                    <emit>
                                        <CNPJ>47896326000102</CNPJ>
                                        <xNome>ISAAC KANASHIRO BARBAGELATA 47497065823</xNome>
                                        <enderNac>
                                            <xLgr>GOVERNADOR PEDRO DE TOLEDO</xLgr>
                                            <nro>57</nro>
                                            <xBairro>BOQUEIRAO</xBairro>
                                            <cMun>3548500</cMun>
                                            <UF>SP</UF>
                                            <CEP>11045551</CEP>
                                        </enderNac>
                                        <fone>1333217406</fone>
                                        <email>ISAACKB1@GMAIL.COM</email>
                                    </emit>
                                    <valores>
                                        <vTotalRet>0.00</vTotalRet>
                                        <vLiq>1512.00</vLiq>
                                    </valores>
                                    <DPS xmlns=""http://www.sped.fazenda.gov.br/nfse"" versao=""1.00"">
                                        <infDPS Id=""DPS354850024789632600010200900000000000000011"">
                                            <tpAmb>1</tpAmb>
                                            <dhEmi>2024-05-03T15:27:36-03:00</dhEmi>
                                            <verAplic>EmissorWeb_1.2.0.3</verAplic>
                                            <serie>900</serie>
                                            <nDPS>11</nDPS>
                                            <dCompet>2024-05-03</dCompet>
                                            <tpEmit>1</tpEmit>
                                            <cLocEmi>3548500</cLocEmi>
                                            <prest>
                                                <CNPJ>47896326000102</CNPJ>
                                                <fone>1333217406</fone>
                                                <email>ISAACKB1@GMAIL.COM</email>
                                                <regTrib>
                                                    <opSimpNac>2</opSimpNac>
                                                    <regEspTrib>0</regEspTrib>
                                                </regTrib>
                                            </prest>
                                            <toma>
                                                <CNPJ>12494939000139</CNPJ>
                                                <IM>410101</IM>
                                                <xNome>DIOGO ATILA RODRIGUES DE CARVALHO</xNome>
                                                <end>
                                                    <endNac>
                                                        <cMun>3541000</cMun>
                                                        <CEP>11718335</CEP>
                                                    </endNac>
                                                    <xLgr>ALVARO SILVA JUNIOR</xLgr>
                                                    <nro>47</nro>
                                                    <xCpl>CASA 47</xCpl>
                                                    <xBairro>QUIETUDE</xBairro>
                                                </end>
                                                <fone>1334721773</fone>
                                                <email>jefersonalisoncontabil@gmail.com</email>
                                            </toma>
                                            <serv>
                                                <locPrest>
                                                    <cLocPrestacao>3548500</cLocPrestacao>
                                                </locPrest>
                                                <cServ>
                                                    <cTribNac>140201</cTribNac>
                                                    <xDescServ>""Prestador de Serviço Abril/24 - 28 horas"" Dados
                                                        Bancários Banco: 0260 - Nu Pagamentos S.A. - Instituição de
                                                        Pagamento Agência: 0001 Conta Corrente: 94945627-0</xDescServ>
                                                </cServ>
                                            </serv>
                                            <valores>
                                                <vServPrest>
                                                    <vServ>1512.00</vServ>
                                                </vServPrest>
                                                <trib>
                                                    <tribMun>
                                                        <tribISSQN>1</tribISSQN>
                                                        <tpRetISSQN>1</tpRetISSQN>
                                                    </tribMun>
                                                    <totTrib>
                                                        <indTotTrib>0</indTotTrib>
                                                    </totTrib>
                                                </trib>
                                            </valores>
                                        </infDPS>
                                    </DPS>
                                </infNFSe>

                                <Signature xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                    <SignedInfo>
                                        <CanonicalizationMethod
                                            Algorithm=""http://www.w3.org/TR/2001/REC-xml-c14n-20010315"" />
                                        <SignatureMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#rsa-sha1"" />
                                        <Reference URI=""#NFS35485002247896326000102000000000001124056512582822"">
                                            <Transforms>
                                                <Transform
                                                    Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" />
                                                <Transform
                                                    Algorithm=""http://www.w3.org/TR/2001/REC-xml-c14n-20010315"" />
                                            </Transforms>
                                            <DigestMethod Algorithm=""http://www.w3.org/2000/09/xmldsig#sha1"" />
                                            <DigestValue>WWKusI9Lbw9QRVinhXjVCXwaYfc=</DigestValue>
                                        </Reference>
                                    </SignedInfo>
                                    <SignatureValue>
                                        ZztQbh0x3eAaS2whZi2pdIENuBfMYvIa+AKiQ4XIdClMavg1CVgtknf6HhsG1WQTxo3tT4AAvKmegb/9ojH+eYiPDAE09HhN7itBxxFAFv9iiirj360PDENaTXhOOEordS/opc6LWThPFEWwGZGA5fIHu5rm1ZLKiODbuRoc4gnZfyjmRvamolpobrLd5I8KP52TgXr6Rxd/HzlJD0mIe4SRuF4xAnt6o2vgaQ1MHtx8cj8aRV54x7y/Rqy3g7uEwjoN646rw5g/kT0fABhoPU2of+7+gomoOy+w/FkfEPociw29z9iNaJ5SDKtqj6V1tazywn9k5wGlwOBcNSbCVA==</SignatureValue>
                                    <KeyInfo>
                                        <X509Data>
                                            <X509Certificate>
                                                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</X509Certificate>
                                        </X509Data>
                                    </KeyInfo>
                                </Signature>
                            </nfe:nfeDadosMsg>
                        </soap:Body>
                    </soap:Envelope>
                "
            );
            return soapEnvelopeDocument;
        }

        private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
    }
}