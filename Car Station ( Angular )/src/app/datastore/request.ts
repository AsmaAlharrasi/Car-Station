import { Time } from "@angular/common";

export interface Request {
    id: string;
    status: string;
    date: Date;
    time: Time;
    customerId: string;
    adminId: string;
    employeeId: string;
    carId: string;
    serviceId : string;
    comment:string;
}
