using RDP.MODEL.ProductionPLanning;
using RDP.UI.Target_Management;
using System;
using System.Windows.Forms;


namespace RDP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
           // Application.Run(new BatchList());  //Live()
            //Application.Run(new  frmItemSetup());  //Live
            Application.Run(new frmLogin());  //Live

          // Application.Run(new frmRSPCalculation());  //Live
          
         //    Application.Run(new frmTargetManagement("3216","0",0));  //Live
          //   Application.Run(new frmTargetManagement("3222","0",0));  //Live




            //Application.Run(new frmUIProfileMaintenance());
            //Application.Run(new frmAssignUIProfile());
            // Application.Run(new frmEarningDeductionApplied(null));


            //Application.Run(new FrmSalaryIntegrationProcess());

            //Application.Run(new frmEarningDeductionApplied(null));

            //Application.Run(new frmValidationForOpProcess());
            //Application.Run(new frmDocumentReserve());
            //Application.Run(new frmOPMonthlyProcess(null));
            //Application.Run(new frmOPCalculationBatchListMaster());

            //Application.Run(new frmBonusPaymentWizardMaster());
            //Application.Run(new frmGradeWiseSecurity());
            //Application.Run(new frmSalaryCalculationBatchListMaster());
            //Application.Run(new frmTaxAdjustmentManualMaster());
            //Application.Run(new frmBonusAdjustmentMaster());
            //Application.Run(new frmValidationForSalaryProcess());
            //Application.Run(new frmSalaryWithheldMaster());
            // Application.Run(new frmIncomeTaxMinimumTaxMaster());
            //Application.Run(new frmLoanScheduleMaster());


        }
    }
}


