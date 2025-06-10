using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExternalAuth.Data;

public class ExternalAuthDbContext(DbContextOptions<ExternalAuthDbContext> options) : IdentityDbContext(options)
{}