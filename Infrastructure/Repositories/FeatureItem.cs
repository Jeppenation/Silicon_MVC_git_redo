using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class FeatureItemRepository(DataContext context) : Repo<FeatureItemRepository>(context)
{
    private readonly DataContext _context = context;
}
