﻿using System.Collections;

namespace ArtGallery;

public interface IRedisRepository {
	Task<bool> Store<T>(string key, T values);
	Task<string?> Get(string key);
}
