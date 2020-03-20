using SYS_MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMPLOYEE;

namespace EKANBAN_SYS_LIB.InterfaceQuery
{
    public interface IEmployeeQuery
    {
        EmployeeContext EmployeeContext { get; }
        Employee GetEmployee(int _employeeId /*Index Id or employeeCode*/);
        Employee GetEmployee(string _rfidNumber);
        ICollection<Employee> GetEmployees(int _buidingId);
        bool AddNewEmployee(Employee _employee);
        AppUser GetAppUser(string _userName, string _passWord);
        AppUser GetAppUser(int _userId);
        bool AddAppUser(AppUser _user);
        ICollection<Employee> GetEmployee(Department _department);
        ICollection<Employee> GetEmployee(Position _position);
        ICollection<Employee> GetEmployee(JobTitle _jobtile);
        bool AddJobTitle(JobTitle _jobtile);
        bool AddPosition(Position _position);
        bool AddDepartment(Department _department);

        bool UpdateEmployee(Employee _employee);
        bool UpdateDepartment(Department _department);
        bool UpdateJobtile(JobTitle _jobtitle);
        bool UpdatePostion(Position _postion);
        bool UpdateAppUser(AppUser _appUser);

    }
}
