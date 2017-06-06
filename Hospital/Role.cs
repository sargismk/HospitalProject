using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{
    enum Role
    {
        DOCTOR,
        PATIENT,
        ADMIN,
    }
    enum Command
    {
        Nothing,
        SignUp=1,
        SignIn,
        Calendar,
        PatientHistory,
        ServeAPatient,
        RequestForConsultation,
        AddDoctor,
        MyRequest,
        Request,
        Reports,
    }
    enum PatientCommand
    {
        RequestForConsultation=1,//++
        MyRequest,//+
        PatientHistory,
    }
    enum DoctorCommand
    {
        Calendar=1,
        MyRequest,//+
        PatientHistory,
        ServeAPatient,
    }
    enum AdminCommand
    {
        AddDoctor=1,
        Reports,
    }
}
