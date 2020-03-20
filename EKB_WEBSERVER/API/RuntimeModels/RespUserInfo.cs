using EKANBAN_SYS_LIB;
using SYS_MODELS;
using SYS_MODELS._ENUM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SYS_MODELS;
using EMPLOYEE;

namespace EKB_WEBSERVER.API.RuntimeModels
{
    public class RespUserInfo
    {
        EmployeeQuery employeeQuery;
        private IDbName Database;
        public RespUserInfo(IDbName database, NewUser new_user)
        {
            employeeQuery = new EmployeeQuery(database);

            var checkUser = employeeQuery.GetEmployee(new_user.RFID);

            if(checkUser!=null)
            {
                Exception = new RespException(true, "Worker has been added", EKB_SYS_REQUEST.GET_USER_INFO);
                return;
            }

            var temp1 = employeeQuery.GetEmployee(new_user.Code);

            int id = 0;
            if (temp1 != null)
            {
                temp1.RFID_Code = new_user.RFID;
                employeeQuery.UpdateEmployee(temp1);
                id = temp1.id;
            }
            else
            {
                var dep = employeeQuery.GetDepartment(new_user.Dep);
                if (dep == null)
                {
                    dep = employeeQuery.AddNewDepartment(new_user.Dep);

                    if (dep == null)
                    {
                        Exception = new RespException(true, "Can not add new department", EKB_SYS_REQUEST.GET_USER_INFO);
                        return;
                    }
                }

                Employee employee = new Employee
                {
                    Name = new_user.Name,
                    RFID_Code = new_user.RFID,
                    Department_Id = dep.id,
                    Position_Id = 3,
                    JobTitle_Id = 2,
                    Building_Id = 1,
                    UserCode = new_user.Code,
                };

                var NewEmployee = employeeQuery.AddNewBeamWorker(employee);

                if (NewEmployee == null)
                {
                    Exception = new RespException(true, "Can not add new user", EKB_SYS_REQUEST.GET_USER_INFO);
                    return;
                }

                id = NewEmployee.id;
            }

            var empl = employeeQuery.GetEmployee(id);

            id = empl.id;
            UserName = ShareFuncs.ConvertToUnSign(empl.Name);
            Pass = true;
            UserCode = empl.UserCode;
            UserRFID = empl.RFID_Code;
            Department = empl.Department.Code;
            JobTitle = empl.JobTitle.Job;

        }

        public RespUserInfo(IDbName database, string rfid)
        {
            employeeQuery = new EmployeeQuery(database);

            var employee = employeeQuery.GetEmployee(rfid);
            if (employee == null)
            {
                Exception = new RespException(true, "Invalid RFID number", EKB_SYS_REQUEST.GET_USER_INFO);
                return;
            }
            id = employee.id;
            UserName = ShareFuncs.ConvertToUnSign(employee.Name);
            Pass = true;
            UserCode = employee.UserCode;
            UserRFID = employee.RFID_Code;
            Department = employee.Department.Code;
            JobTitle = employee.JobTitle.Job;
        }
        public int id { get; set; }
        public int UserCode { get; set; }
        public string UserName { get; set; }
        public string UserRFID { get; set; }
        public bool Pass { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }

        public RespException Exception = new RespException(false, "Get User Info: OK", EKB_SYS_REQUEST.GET_USER_INFO);
    }
}