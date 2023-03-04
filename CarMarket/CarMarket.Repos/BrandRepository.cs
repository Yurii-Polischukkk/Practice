﻿using CarMarket.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket.Repos
{
    public class BrandRepository
    {
        private readonly CarMarketDbContext _ctx;

        public BrandRepository(CarMarketDbContext _ctx)
        {
            this._ctx = _ctx;
        }

        public async Task<Brand> AddBrandAsync(Brand brand)
        {
            _ctx.Brands.Add(brand);
            await _ctx.SaveChangesAsync();
            return _ctx.Brands.FirstOrDefault(x => x.Name == brand.Name);
        }

        public Brand GetBrand(int id)
        {
            return _ctx.Brands.FirstOrDefault(x => x.Id == id);
        }
        public Brand GetBrandByName(string name)
        {
            var brand = _ctx.Brands.FirstOrDefault(x=> x.Name== name);
            return brand;
        }

        public List<Brand> GetBrands()
        {
            var brandList = _ctx.Brands.ToList();
            return brandList;
        }

        public async Task DeleteBrandAsync(int id)
        {
            _ctx.Remove(GetBrand(id));
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateBrandAsync(Brand updatedBrand)
        {
            var brand = _ctx.Brands.FirstOrDefault(x => x.Id == updatedBrand.Id);
            brand.Description = updatedBrand.Description;
            brand.Name = updatedBrand.Name;
            await _ctx.SaveChangesAsync();
        }
    }
}
