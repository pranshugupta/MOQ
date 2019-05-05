using System;
using System.Collections.Generic;

namespace ClassLibraryMoq1
{
    public enum StatusLevel
    {
        Default,
        Platinum
    }
    public class CustomerService1
    {
        private readonly ICustomerRepository customerRepository;
        private readonly ICustomerAddresBuilder customerAddresBuilder;
        private readonly IIdCreator idCreator;
        private readonly IFullNameBuilder fullNameBuilder;
        private readonly IStatusFactory statusFactory;
        private readonly IAppSetting appSetting;
        private readonly IMalingRepository malingRepository;
        private readonly IComplexSetting complexSetting;
        private readonly IAddressFormatterFactory addressFormatterFactory;
        public CustomerService1(ICustomerRepository customerRepository, IMalingRepository malingRepository)
        {
            this.customerRepository = customerRepository;
            this.malingRepository = malingRepository;
            this.customerRepository.OnNotify += CustomerRepository_OnNotify;

        }


        public CustomerService1(ICustomerRepository customerRepository, IAddressFormatterFactory addressFormatterFactory)
        {
            this.customerRepository = customerRepository;
            this.addressFormatterFactory = addressFormatterFactory;

        }

        public CustomerService1(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;

        }

        public CustomerService1(ICustomerRepository customerRepository, IAppSetting appSetting)
        {
            this.customerRepository = customerRepository;
            this.appSetting = appSetting;

        }
        public CustomerService1(ICustomerRepository customerRepository, IComplexSetting complexSetting)
        {
            this.customerRepository = customerRepository;
            this.complexSetting = complexSetting;

        }
        public CustomerService1(ICustomerRepository customerRepository, ICustomerAddresBuilder customerAddresBuilder)
        {
            this.customerRepository = customerRepository;
            this.customerAddresBuilder = customerAddresBuilder;

        }

        public CustomerService1(ICustomerRepository customerRepository, IIdCreator idCreator)
        {
            this.customerRepository = customerRepository;
            this.idCreator = idCreator;

        }

        public CustomerService1(ICustomerRepository customerRepository, IFullNameBuilder fullNameBuilder)
        {
            this.customerRepository = customerRepository;
            this.fullNameBuilder = fullNameBuilder;

        }
        public CustomerService1(ICustomerRepository customerRepository, IStatusFactory statusFactory)
        {
            this.customerRepository = customerRepository;
            this.statusFactory = statusFactory;

        }
        public void Create(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);
            customerRepository.Save(customer);
        }

        public void CreateMany(IEnumerable<CustomerToCreateDto> customersToCreateDto)
        {
            foreach (var customerToCreate in customersToCreateDto)
            {
                var customer = BuildCustomerObjectFrom(customerToCreate);
                customerRepository.Save(customer);
            }
        }

        public void CreateWithAddress(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);

            customer.MailingAddress = customerAddresBuilder.From(customerToCreateDto);

            if (customer.MailingAddress == null)
            {
                throw new InvalidCustomerMailingAddressException();
            }
            customerRepository.Save(customer);
        }
        private Customer BuildCustomerObjectFrom(CustomerToCreateDto customerToCreateDto)
        {
            return new Customer(customerToCreateDto.FirstName, customerToCreateDto.LastName, customerToCreateDto.City);
        }

        public void CreateWithAddressOut(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);

            Addres mailingAddress =

            customer.MailingAddress = customerAddresBuilder.From(customerToCreateDto);

            customerAddresBuilder.TryParse(customerToCreateDto.City, out mailingAddress);

            if (mailingAddress == null)
            {
                throw new InvalidCustomerMailingAddressException();
            }
            customer.MailingAddress = mailingAddress;
            customerRepository.Save(customer);
        }

        public void CreateNewCustomerIdEachTime(IEnumerable<CustomerToCreateDto> customerToCreateDtos)
        {
            foreach (var customerToCreateDto in customerToCreateDtos)
            {
                var customer = BuildCustomerObjectFrom(customerToCreateDto);
                customer.Id = idCreator.Create();

                customerRepository.Save(customer);
            }
        }

        public void CreateFullNameCustomer(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);
            customer.FullName = fullNameBuilder.From(customerToCreateDto.FirstName, customerToCreateDto.LastName);

            customerRepository.Save(customer);
        }

        public void CreateCustomerWithStatus(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);

            customer.StatusLevel = statusFactory.CreateFrom(customerToCreateDto);
            if (customer.StatusLevel == StatusLevel.Platinum)
            {
                customerRepository.SaveSpecial(customer);
            }
            else
            {
                customerRepository.Save(customer);
            }
        }

        public void CreateThrowException(CustomerToCreateDto customerToCreateDto)
        {
            try
            {
                var customer = BuildCustomerObjectFrom(customerToCreateDto);
                customer.MailingAddress = customerAddresBuilder.From(customerToCreateDto);
                customerRepository.Save(customer);
            }
            catch (InvalidCustomerMailingAddressException e)
            {
                throw new CustomerCreationException(e);
            }
        }

        public void CreateWithTime(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);
            customerRepository.LocalTimeZone = TimeZone.CurrentTimeZone.StandardName;
            customerRepository.Save(customer);
        }

        public void CreateGetCall(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);
            var workStationId = appSetting.WorkStationId;

            if (!workStationId.HasValue)
            {
                throw new InValidWorkStationException();
            }
            customer.WorkStationId = workStationId;
            customerRepository.Save(customer);
        }

        public void CreateGetHierarchy(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);
            var workStationId = complexSetting.SystemConfiguration.AuditingInformation.WorkStationId;

            if (!workStationId.HasValue)
            {
                throw new InValidWorkStationException();
            }
            customer.WorkStationId = workStationId;
            customerRepository.Save(customer);
        }

        public void CreateStubProperty(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);
            var workStationId = appSetting.WorkStationId;

            if (!workStationId.HasValue)
            {
                throw new InValidWorkStationException();
            }
            customer.WorkStationId = workStationId;
            customerRepository.Save(customer);
        }

        public void CreateAndNotify(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);
            customerRepository.Save(customer);
        }
        private void CustomerRepository_OnNotify(object sender, EventArgs e)
        {
            malingRepository.Mail(e);
        }

        public void CreateAndFetch(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);
            customerRepository.Save(customer);
            customerRepository.FetchAll();
        }

        public void CreateUsingFactory(CustomerToCreateDto customerToCreateDto)
        {
            var customer = BuildCustomerObjectFrom(customerToCreateDto);
            ICustomerAddresBuilder addressBuilder = addressFormatterFactory.From(customerToCreateDto);

            customer.MailingAddress = addressBuilder.From(customerToCreateDto);

            customerRepository.Save(customer);
        }
    }
}
