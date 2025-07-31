namespace backend.Models;
using System;
using System.Collections.Generic;

public class Page<T>
{
	public IEnumerable<T> Items { get; set; }
	public int TotalCount { get; set; }
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
}
