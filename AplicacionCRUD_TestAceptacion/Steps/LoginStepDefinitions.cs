using Microsoft.Edge.SeleniumTools;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Data.SqlClient;
using TechTalk.SpecFlow;

namespace AplicacionCRUD_TestAceptacion.Steps
{
    [Binding]
    public sealed class LoginStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;

        IWebDriver _driver;

        string urlDestino = "https://localhost:5001";

        string email = "kyron.meza@outlook.com";
        string password = "asdf1234";

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef


        public LoginStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"entro a la pagina de la AplicacionCRUD")]
        public void GivenEntroALaPaginaDeLaAplicacionCRUD()
        {
            //INICIALIZAR EL DRIVER DEL NAVEGADOR
            //Utilizando Edge Chromium
            EdgeOptions edgeOptions = new EdgeOptions();
            edgeOptions.UseChromium = true;
            edgeOptions.AcceptInsecureCertificates = true;
            _driver = new EdgeDriver(edgeOptions);

            _driver.Manage().Window.Maximize();

            _driver.Url = urlDestino;

            System.Threading.Thread.Sleep(2000);
        }

        [Then(@"Escribo email")]
        public void ThenEscriboEmail()
        {
            //Escribir email
            IWebElement loginEmail = _driver.FindElement(By.Name("email"));
            loginEmail.SendKeys(email);
        }

        [Then(@"Escribo password")]
        public void ThenEscriboPassword()
        {
            //Escribir email
            IWebElement loginPassword = _driver.FindElement(By.Name("password"));
            loginPassword.SendKeys(password);
        }

        [Then(@"Submit")]
        public void ThenSubmit()
        {
            IWebElement submitButton = _driver.FindElement(By.XPath("html/body/div/main/div/form/div/div/div[3]/button"));
            submitButton.Click();
            System.Threading.Thread.Sleep(1000);
        }

        [Then(@"Verificar que estoy logeado")]
        public void ThenVerificarQueEstoyLogeado()
        {
            var cookieVar = _driver.Manage().Cookies.GetCookieNamed("SessionID");
            Assert.NotNull(cookieVar);
            System.Threading.Thread.Sleep(1000);
        }

        [Then(@"Entro al Index de la AplicacionCRUD")]
        public void GivenEntroAlIndexDeLaAplicacionCRUD()
        {
            _driver.Navigate().GoToUrl("https://localhost:5001/");
            System.Threading.Thread.Sleep(1000);
        }

        [Then(@"Selecciono el menu Usuarios")]
        public void ThenSeleccionoElMenuUsuarios()
        {
            IWebElement usuariosLink = _driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul/li[5]/a"));
            usuariosLink.Click();
            System.Threading.Thread.Sleep(1000);
        }

        [Then(@"Selecciono Agregar Usuario")]
        public void ThenSeleccionoAgregarUsuario()
        {
            IWebElement createNewLink = _driver.FindElement(By.LinkText("Create New"));
            createNewLink.Click();
            System.Threading.Thread.Sleep(1000);

        }
        
        [Then(@"Relleno la informacion del Usuario")]
        public void ThenRellenoLaInformacionDelUsuario()
        {
            _driver.FindElement(By.Id("CodigoUsuario")).SendKeys("99888777-6");
            _driver.FindElement(By.Id("Email")).SendKeys("test@test.test");
            _driver.FindElement(By.Id("Password")).SendKeys("test1234");
            _driver.FindElement(By.Id("Nombre")).SendKeys("TEST Name");
            _driver.FindElement(By.Id("Apellido")).SendKeys("TEST Last Name");
        }

        [Then(@"Submit Usuario")]
        public void ThenSubmitUsuario()
        {
            _driver.FindElement(By.XPath("//input[@type='submit' and @value='Create']")).Click();
        }

        [Then(@"Verificar que el usuario haya sido creado")]
        public void ThenVerificarQueElUsuarioHayaSidoCreado()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = "Server=.\\SQLEXPRESS;Database=sistemaUsuarios;Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            string query = "SELECT * FROM usuarios WHERE codigoUsuario = '99888777-6'";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            var result = reader.HasRows;
            reader.Close();
            Assert.IsTrue(result);
        }

        [Then(@"Verificar que el usuario haya sido creado2")]
        public void ThenVerificarQueElUsuarioHayaSidoCreado2()
        {
            int result = 0;
            var tds = _driver.FindElements(By.TagName("td"));
            foreach (var td in tds)
            {
                if (td.Text == "test@test.test")
                {
                    result += 1;
                }
            }
            Assert.NotZero(result);
        }

        [Then(@"Eliminar el Usuario Creado")]
        public void ThenEliminarElUsuarioCreado()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = "Server=.\\SQLEXPRESS;Database=sistemaUsuarios;Trusted_Connection=True;";
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            string query = "DELETE FROM usuarios WHERE codigoUsuario = '99888777-6'";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            var deleteResult = reader.RecordsAffected;
            reader.Close();
            Assert.IsTrue(deleteResult >= 1);
        }

        //TODO --> Eliminar el Usuario Creado Mediante la App


        [Then(@"Cerrar el navegador")]
        public void ThenCerrarElNavegador()
        {
            _driver.Quit();
        }



        //[Then(@"Cerrar navegador")]
        //public void ThenCerrarNavegador()
        //{
        //    _driver.Quit();
        //}


    }
}
