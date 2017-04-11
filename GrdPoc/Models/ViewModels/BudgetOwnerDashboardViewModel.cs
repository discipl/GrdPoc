using GrdPoc.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GrdPoc.Models.ViewModels
{
    public class BudgetOwnerDashboardViewModel
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<IncidentalContract> ContractsInNegotiationList { get; set; }

        public List<IncidentalContract> ContractsInApprovalList { get; set; }

        public List<BudgetViewModel> BudgetsList { get; private set; }

        public void SetBudgetsList(List<Budget> budgetList)
        {
            BudgetsList = new List<BudgetViewModel>();
            if (budgetList.Count() != 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    var budget = new BudgetViewModel();

                    budget.BudgetName = budgetList[i].BudgetName;
                    //budget.InitialBudget = budgetList[i].BudgetInitialBalance.ToString("C", CultureInfo.CreateSpecificCulture("nl-NL"));
                    budget.InitialBudget = budgetList[i].BudgetInitialBalance.ToString("C", CultureInfo.CurrentCulture);
                    budget.CurrentBudget = budgetList[i].BudgetBalance.ToString("C", CultureInfo.CurrentCulture);

                    var budgetCodeId = budgetList[i].BudgetIdCode;
                    try
                    {
                        var outsatndingBudgetInOpenContracts = db.IncidentalContractItems.Where(w => w.IncidentalContract.IncidentalContracStatus == ContractStatus.Confirmed
                                                                                                  && w.BudgetCodeId == budgetCodeId)
                                                                                         .Sum(s => s.IncidentalContractItemValue * s.IncidentalContractItemQuantity );
                        budget.OutstandingBudget = outsatndingBudgetInOpenContracts.ToString("C", CultureInfo.CurrentCulture);

                        budget.CurrentBudget = (budgetList[i].BudgetBalance - outsatndingBudgetInOpenContracts).ToString("C", CultureInfo.CurrentCulture);

                        budget.BudgetsAvailablePercentage = Convert.ToInt32( ((budgetList[i].BudgetBalance - outsatndingBudgetInOpenContracts) / budgetList[i].BudgetInitialBalance) * 100);

                    }
                    catch //(Exception ex)
                    {
                        budget.OutstandingBudget = 0M.ToString("C", CultureInfo.CurrentCulture);
                        budget.BudgetsAvailablePercentage = 100;
                    }

                    var budgetId = budgetList[i].BudgetId;

                    var budgetTransactions = db.BudgetBalanceTransactions.Where(w => w.OwnerBudgetId == budgetId).ToList();

                    string valuesArrayString = budgetList[i].BudgetInitialBalance.ToString("G", CultureInfo.CreateSpecificCulture("en-US"));
                    decimal valuesSpent = 0M;
                    foreach (var item in budgetTransactions)
                    {
                        if (valuesArrayString == "")
                            valuesArrayString = (budgetList[i].BudgetInitialBalance - item.BudgetBalanceWithdrawal).ToString("G", CultureInfo.CreateSpecificCulture("en-US"));
                        else
                            valuesArrayString += ", " + (budgetList[i].BudgetInitialBalance - item.BudgetBalanceWithdrawal).ToString("G", CultureInfo.CreateSpecificCulture("en-US"));

                        valuesSpent += item.BudgetBalanceWithdrawal;
                    }
                    if (valuesArrayString == budgetList[i].BudgetInitialBalance.ToString("G", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        valuesArrayString += ", " + budgetList[i].BudgetInitialBalance.ToString("G", CultureInfo.CreateSpecificCulture("en-US"));
                    }
                    budget.ValuesArray = valuesArrayString;
                    budget.BudgetSpent = valuesSpent.ToString("C", CultureInfo.CurrentCulture);


                    BudgetsList.Add(budget);
                }
            }
        }
    }
}