// Copyright © 2022 Avery Dennison. All Rights Reserved.

namespace BookStore.Infrastructure;

public class BaseRepository
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}
