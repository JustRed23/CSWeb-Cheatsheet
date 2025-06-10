using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasicAuth.Data;

public class BasicAuthDbContext(DbContextOptions<BasicAuthDbContext> options) : IdentityDbContext(options)
{}