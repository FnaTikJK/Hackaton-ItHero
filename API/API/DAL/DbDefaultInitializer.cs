using System.Runtime.CompilerServices;
using API.Modules.SpecializationsModule.Entity;

namespace API.DAL;

public class DbDefaultInitializer
{
  private static DataContext dataContext;
  public static void FillDbByDefaultValues(DataContext dataContext)
  {
    DbDefaultInitializer.dataContext = dataContext;
    AddSpecializations();
    DbDefaultInitializer.dataContext.SaveChanges();
  }

  private static void AddSpecializations()
  {
    var specNames = new[]
    {
      "Комплексное управление ресурсами предприятия (ERP)",
      "Управление взаимоотношениями с клиентами (CRM) и маркетингом",
      "Зарплата, управление персоналом и кадровый учет (HRM)",
      "Управленческий и финансовый учет (FRP)",
      "Управление продажами, логистикой и транспортом (SFM, WMS, TMS)",
      "Управление инженерными данными и НСИ (PDM, MDM)",
      "Управление информационными технологиями (ITIL)",
      "Управление ремонтами (CMM, EAM)",
      "Управление проектами и портфелями проектов (PMO, EPM)",
      "Электронное обучение (e-Learning)",
      "Бухгалтерский и налоговый учет",
      "Документооборот",
      "Охрана труда и окружающей среды, безопасность (EHS)",
    };
    foreach (var specName in specNames)
    {
      dataContext.Specializations.Add(Spec(specName));
    }

    SpecializationEntity Spec(string name) => new() { Name = name };
  }
}
