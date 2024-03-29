﻿using ASPortStore.Models;
using ASPortStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPortStore.Controllers;

public class HomeController(IStoreRepository storeRepository) : Controller
{
    private readonly IStoreRepository storeRepository = storeRepository;

    public int PageSize { get; set; } = 2;

    public ViewResult Index(string? category, int page = 1)
        => View(new ProductsListViewModel
        {
            Products = storeRepository.Products
                                      .OrderBy(product => product.ProductID)
                                      .Where(product => category == null || product.Category == category)
                                      .Skip((page - 1) * PageSize)
                                      .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = storeRepository.Products
                                            .Where(product => category == null || product.Category == category)
                                            .Count(),
            },
            CurrentCategory = category,
        });
}