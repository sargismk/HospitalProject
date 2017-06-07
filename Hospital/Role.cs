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
    enum PatientCommand
    {
        RequestForConsultation=1,//++
        MyRequest,//+ um mot petq lini jam or
        PatientHistory,//um mota exel jam or
    }
    enum DoctorCommand
    {
        Calendar=1,//ov petqa lini jam or
        MyRequest,//+
        PatientHistory,//ova exel jam or
    }
    enum AdminCommand
    {
        AddDoctor=1,//+
        Reports,
    }
}
