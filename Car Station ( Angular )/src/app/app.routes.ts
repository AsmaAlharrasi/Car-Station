import { Routes } from "@angular/router";
import { LoginComponent } from "./authentication/login/login.component";
import { RegistComponent } from "./authentication/regist/regist.component";
import { AddCarComponent } from "./car/add-car/add-car.component";
import { EditCarComponent } from "./car/edit-car/edit-car.component";
import { ViewAllCarsComponent } from "./car/view-all-cars/view-all-cars.component";
import { AddServiceComponent } from "./carServices/add-service/add-service.component";
import { AdminViewAllServiceComponent } from "./carServices/admin-view-all-service/admin-view-all-service.component";
import { EditServiceComponent } from "./carServices/edit-service/edit-service.component";
import { ViewAllServiceComponent } from "./carServices/view-all-service/view-all-service.component";
import { ViewServiceComponent } from "./carServices/view-service/view-service.component";
import { AdminEditRequestComponent } from "./request/admin-request/admin-edit-request/admin-edit-request.component";
import { AdminViewAllRequestComponent } from "./request/admin-request/admin-view-all-request/admin-view-all-request.component";
import { CustomerAddRequestComponent } from "./request/customer-request/customer-add-request/customer-add-request.component";
import { CustomerViewAllRequestComponent } from "./request/customer-request/customer-view-all-request/customer-view-all-request.component";
import { EmployeeViewAllRequestComponent } from "./request/employee-request/employee-view-all-request/employee-view-all-request.component";
import { AboutComponent } from "./shared/about/about.component";
import { HomeComponent } from "./shared/home/home.component";
import { AddCustomerComponent } from "./users/customers/add-customer/add-customer.component";
import { AdminEditCustomerComponent } from "./users/customers/admin-edit-customer/admin-edit-customer.component";
import { EditProfileCustomerComponent } from "./users/customers/edit-profile-customer/edit-profile-customer.component";
import { ViewAllCustomerComponent } from "./users/customers/view-all-customer/view-all-customer.component";
import { ViewProfileCustomerComponent } from "./users/customers/view-profile-customer/view-profile-customer.component";
import { AddEmployeeComponent } from "./users/employees/add-employee/add-employee.component";
import { EditProfileEmployeeComponent } from "./users/employees/edit-profile-employee/edit-profile-employee.component";
import { ViewAllEmployeeComponent } from "./users/employees/view-all-employee/view-all-employee.component";
import { ViewProfileEmployeeComponent } from "./users/employees/view-profile-employee/view-profile-employee.component";




export const routes: Routes = [

    //All User Routes
    { path: "", component: HomeComponent },
    { path: "Login", component: LoginComponent },
    { path: "Signup", component: RegistComponent },
    { path: "About", component: AboutComponent },

    //Service Routes
    { path: "ViewService/:id", component: ViewServiceComponent },
    { path: "ViewAllServices", component: ViewAllServiceComponent },
    { path: "EditService/:Id", component: EditServiceComponent },
    { path: "AdminViewServices", component: AdminViewAllServiceComponent },




    //Customer Routes
    { path: "ViewCustomerRequest/:CustId", component: CustomerViewAllRequestComponent },
    { path: "ViewCustomerProfile", component: ViewProfileCustomerComponent },
    { path: "CustomerAddRequest/:CustId", component: CustomerAddRequestComponent },
    { path: "EditCustomerProfile/:Id", component: EditProfileCustomerComponent },
    

    //Car Routes
    { path: "AddCar/:CustId", component: AddCarComponent },
    { path: "ViewCars", component: ViewAllCarsComponent },
    { path: "EditCar/:Id", component: EditCarComponent },



  //Employee Routes
  {path:"AddEmployee", component: AddEmployeeComponent},
  {path:"EditEmployee", component: EditProfileEmployeeComponent},
  {path:"ViewEmployee", component: ViewProfileEmployeeComponent},
  {path:"ViewEmployeeRequests/:EmpId", component: EmployeeViewAllRequestComponent},
  {path:"ViewAllEmployeeComponent", component: ViewAllEmployeeComponent},
  {path:"EditEmployee/:EmpId", component : EditProfileEmployeeComponent},



    //Admin Routs
    { path: "ViewAllCustomers", component: ViewAllCustomerComponent },
    { path: "NewCustomer", component: AddCustomerComponent },
    { path: "ViewAllService", component: AdminViewAllServiceComponent },
    { path: "AddNewService", component: AddServiceComponent },
    { path: "EditService/:id", component: EditServiceComponent },
    { path: "ViewRequest", component: AdminViewAllRequestComponent },
    { path: "EditCustomer/:Id", component: AdminEditCustomerComponent },
    { path: "EditRequest/:Id", component: AdminEditRequestComponent },



];