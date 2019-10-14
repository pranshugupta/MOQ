using ClassLibraryMoq1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTestProjectMoq1
{
    [TestFixture]
    public class CustomerService1Test
    {
        [Test]
        public void repository_save_should_be_called()
        {
            //Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(x => x.Save(It.IsAny<Customer>()));
            var customerService = new CustomerService1(mockRepository.Object);

            //Act
            customerService.Create(new CustomerToCreateDto());

            //Assert    It verifies all the things in arrange
            Mock.VerifyAll();
        }


        [Test]
        public void repository_save_should_be_called_multiple_times()
        {
            //Arrange
            var customers = new List<CustomerToCreateDto>() {
                    new CustomerToCreateDto(){FirstName="Pranshu", City="Naraini" },
                    new CustomerToCreateDto(){FirstName="Pravesh", City="Naraini" }
                };
            var mockRepository = new Mock<ICustomerRepository>();
            var customerService = new CustomerService1(mockRepository.Object);

            //Act
            customerService.CreateMany(customers);

            //Assert
            mockRepository.Verify(x => x.Save(It.IsAny<Customer>()), Times.Exactly(customers.Count));
        }

        [Test]
        [ExpectedException(typeof(InvalidCustomerMailingAddressException))]
        public void repository_save_should_be_called_without_adrress()
        {
            //Arrange
            var customerToCreate = new CustomerToCreateDto() { FirstName = "Pranshu", City = "Naraini" };
            var mockRepository = new Mock<ICustomerRepository>();
            var mockAddressBuilder = new Mock<ICustomerAddresBuilder>();
            mockAddressBuilder.Setup(x => x.From(It.IsAny<CustomerToCreateDto>())).Returns(() => null);

            var customerService = new CustomerService1(mockRepository.Object, mockAddressBuilder.Object);

            //Act
            customerService.CreateWithAddress(customerToCreate);

            //Assert    It verifies all the things in arrange
            //  Mock.VerifyAll();
        }

        [Test]
        public void repository_save_should_be_called_with_adrress()
        {
            //Arrange
            var customerToCreate = new CustomerToCreateDto() { FirstName = "Pranshu", City = "Naraini" };
            var mockRepository = new Mock<ICustomerRepository>();
            var mockAddressBuilder = new Mock<ICustomerAddresBuilder>();
            mockAddressBuilder.Setup(x => x.From(It.IsAny<CustomerToCreateDto>())).Returns(() => new Addres());

            var customerService = new CustomerService1(mockRepository.Object, mockAddressBuilder.Object);

            //Act
            customerService.CreateWithAddress(customerToCreate);

            //Assert 
            mockRepository.Verify(x => x.Save(It.IsAny<Customer>()));
        }

        [Test]
        public void repository_save_should_be_called_with_adrress_out()
        {
            //Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            var mockAddressBuilder = new Mock<ICustomerAddresBuilder>();

            var mailingAddress = new Addres() { City = "Naraini" };
            mockAddressBuilder.Setup(x => x.TryParse(It.IsAny<string>(), out mailingAddress));

            var customerService = new CustomerService1(mockRepository.Object, mockAddressBuilder.Object);

            //Act
            customerService.CreateWithAddressOut(new CustomerToCreateDto());

            //Assert    It verifies all the things in arrange
            mockRepository.Verify(x => x.Save(It.IsAny<Customer>()));
        }

        [Test]
        public void repository_save_should_create_new_id_eachtime()
        {
            //Arrange
            var customerList = new List<CustomerToCreateDto>() {
                new CustomerToCreateDto(){FirstName = "10", City="100" },
                new CustomerToCreateDto(){FirstName = "20", City="200" }
            };
            var mockRpository = new Mock<ICustomerRepository>();
            mockRpository.Setup(x => x.Save(It.IsAny<Customer>()));
            var mockIdCreator = new Mock<IIdCreator>();
            int i = 1;

            mockIdCreator.Setup(x => x.Create())
                .Returns(() => i)
                .Callback(() => i++);

            var customerService = new CustomerService1(mockRpository.Object, mockIdCreator.Object);
            //Act
            customerService.CreateNewCustomerIdEachTime(customerList);

            //Assert
            mockIdCreator.Verify(x => x.Create(), Times.AtLeastOnce());
        }

        [Test]
        void fullname_should_be_created_from_firstName_and_lastName()
        {
            //Arrange
            CustomerToCreateDto cutmr = new CustomerToCreateDto() { FirstName = "Pranshu", LastName = "Gupta" };
            var mockRepository = new Mock<ICustomerRepository>();
            var mockFullnameBuilder = new Mock<IFullNameBuilder>();
            mockFullnameBuilder.Setup(x => x.From(It.IsAny<string>(), It.IsAny<string>()));
            // .Returns(() => "Pranshu Gupta");
            var customerService = new CustomerService1(mockRepository.Object, mockFullnameBuilder.Object);

            //Act
            customerService.CreateFullNameCustomer(cutmr);

            //assert
            mockFullnameBuilder.Verify(x =>
                x.From(
                    It.Is<string>(fn =>
                        fn.Equals(cutmr.FirstName, StringComparison.InvariantCultureIgnoreCase)),
                    It.Is<string>(fn =>
                        fn.Equals(cutmr.LastName, StringComparison.InvariantCultureIgnoreCase))
                    ));
        }

        [Test]
        public void save_special_customer()
        {
            //Arrange
            var mockRepository = new Mock<ICustomerRepository>();
            var mockSatusFactory = new Mock<IStatusFactory>();
            mockSatusFactory.Setup(x => x.CreateFrom(It.IsAny<CustomerToCreateDto>()))
                .Returns(() => StatusLevel.Platinum);


            var customerService = new CustomerService1(mockRepository.Object, mockSatusFactory.Object);
            //Act
            customerService.CreateCustomerWithStatus(new CustomerToCreateDto());
            //Assert
            mockRepository.Verify(x => x.SaveSpecial(It.IsAny<Customer>()));

        }

        [Test]
        [ExpectedException(typeof(CustomerCreationException))]
        public void exception_on_save()
        {
            //arrage
            var mockRepo = new Mock<ICustomerRepository>();

            var mockAddress = new Mock<ICustomerAddresBuilder>();
            mockAddress
                .Setup(x => x.From(It.IsAny<CustomerToCreateDto>()))
                .Throws<InvalidCustomerMailingAddressException>();

            var cusServ = new CustomerService1(mockRepo.Object, mockAddress.Object);

            //Act
            cusServ.CreateThrowException(new CustomerToCreateDto());

            //assert
        }

        [Test]
        public void local_time_zone_should_be_set()
        {
            var mockrepo = new Mock<ICustomerRepository>();
            var customerService = new CustomerService1(mockrepo.Object);

            customerService.CreateWithTime(new CustomerToCreateDto());

            mockrepo.VerifySet(x => x.LocalTimeZone = It.IsAny<string>());
        }

        [Test]
        public void create_get_called()
        {
            var mockrepo = new Mock<ICustomerRepository>();
            var mockappSetting = new Mock<IAppSetting>();
            mockappSetting.Setup(x => x.WorkStationId).Returns(() => 123);
            var customerService = new CustomerService1(mockrepo.Object, mockappSetting.Object);

            customerService.CreateGetCall(new CustomerToCreateDto());

            mockappSetting.VerifyGet(x => x.WorkStationId);
        }

        [Test]
        public void create_get_Hierarchy_called()
        {
            var mockrepo = new Mock<ICustomerRepository>();
            var mockComplesSetting = new Mock<IComplexSetting>();
            mockComplesSetting.Setup(x => x.SystemConfiguration.AuditingInformation.WorkStationId).Returns(() => 123);
            var customerService = new CustomerService1(mockrepo.Object, mockComplesSetting.Object);

            customerService.CreateGetHierarchy(new CustomerToCreateDto());

            mockComplesSetting.VerifyGet(x => x.SystemConfiguration.AuditingInformation.WorkStationId);
        }

        [Test]
        public void create_stub_property()
        {
            var mockrepo = new Mock<ICustomerRepository>();
            var mockappSetting = new Mock<IAppSetting>();
            mockappSetting.SetupProperty(x => x.WorkStationId, 123);
            mockappSetting.Object.WorkStationId = 1234156;
            var customerService = new CustomerService1(mockrepo.Object, mockappSetting.Object);

            customerService.CreateStubProperty(new CustomerToCreateDto());

            mockappSetting.VerifyGet(x => x.WorkStationId);
        }

        [Test]
        public void create_and_notify()
        {
            var mockRepo = new Mock<ICustomerRepository>();
            var mailRepo = new Mock<IMalingRepository>();
            mockRepo.Raise(x => x.OnNotify += null, It.IsAny<EventArgs>());

            var serv = new CustomerService1(mockRepo.Object, mailRepo.Object);
            serv.CreateAndNotify(new CustomerToCreateDto());

            mailRepo.Verify(x => x.Mail(It.IsAny<EventArgs>()));
        }

        [Test]
        public void create_and_fetch()
        {
            //Arrange
            var mockRepository = new Mock<ICustomerRepository>(MockBehavior.Strict); //Strict, It also verifies other methods
            mockRepository.Setup(x => x.Save(It.IsAny<Customer>()));
            var customerService = new CustomerService1(mockRepository.Object);

            //Act
            customerService.Create(new CustomerToCreateDto());

            //Assert    It verifies all the things in arrange
            Mock.VerifyAll();
        }

        [Test]
        public void create_address_using_factory()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerRepository>();
            var mockAddressBuilder = new Mock<ICustomerAddresBuilder>();
            mockAddressBuilder.Setup(x => x.From(It.IsAny<CustomerToCreateDto>()));
            var mockAddrFactry = new Mock<IAddressFormatterFactory>();
            mockAddrFactry.Setup(x => x.From(It.IsAny<CustomerToCreateDto>())).Returns(() => mockAddressBuilder.Object);
            var serv = new CustomerService1(mockRepo.Object, mockAddrFactry.Object);
            //Act
            serv.CreateUsingFactory(new CustomerToCreateDto());
            //Assert
            mockAddressBuilder.Verify(x => x.From(It.IsAny<CustomerToCreateDto>()));
        }

        [Test]
        public void create_address_using_factory2()
        {
            // Arrange
            var mockRepo = new Mock<ICustomerRepository>();
            var mockAddrFactry = new Mock<IAddressFormatterFactory>() { DefaultValue = DefaultValue.Mock };
            var addressBuilder = mockAddrFactry.Object.From(It.IsAny<CustomerToCreateDto>());
            var mockAddressBuilder = Mock.Get(addressBuilder);
            var serv = new CustomerService1(mockRepo.Object, mockAddrFactry.Object);
            //Act
            serv.CreateUsingFactory(new CustomerToCreateDto());
            //Assert
            mockAddressBuilder.Verify(x => x.From(It.IsAny<CustomerToCreateDto>()));
        }

        [Test]
        public void create_address_using_factory3()
        {
            var mockFactory = new MockRepository(MockBehavior.Loose) { DefaultValue = DefaultValue.Mock };
            var mockcustomerRepo = mockFactory.Create<ICustomerRepository>();
            var mockAddressFactory = mockFactory.Create<IAddressFormatterFactory>();


            mockFactory.VerifyAll();
        }
    }
}
