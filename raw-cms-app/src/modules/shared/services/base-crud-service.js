import { mix } from '../../../utils/inheritance.utils.js';
import { optionalChain } from '../../../utils/object.utils.js';
import { BaseApiService } from './base-api-service.js';
import { ICrudService } from './crud-service.js';

export class BaseCrudService extends mix(BaseApiService, ICrudService) {
  _basePath;

  constructor({ basePath }) {
    super();
    this._basePath = basePath;
  }

  async getAll() {
    const res = await this.getPage({ size: Number.MAX_VALUE });
    return res.items;
  }

  async getPage({ page = 1, size = 20, rawQuery = undefined, sort = undefined } = {}) {
    const config = { pageSize: size, pageNumber: page };
    if (rawQuery) {
      config.rawQuery = rawQuery;
    }
    if (sort) {
      config.sort = JSON.stringify(sort);
    }
    const res = await this._apiClient.get(this._basePath, { params: config });
    return res.data.data;
  }

  async getById(id) {
    if (!id) {
      console.error(`Unable to get item: id is invalid (${obj})`);
      return false;
    }

    const res = await this._apiClient.get(`${this._basePath}/${id}`);

    if (!this._checkGenericError(res)) {
      return false;
    }

    return res.data.data;
  }

  async create(obj) {
    const id = optionalChain(() => obj._id);

    if (id) {
      console.error(`Unable to create item: object has an id (${obj})`);
      return false;
    }

    const res = await this._apiClient.post(`${this._basePath}`, obj);
    return this._checkGenericError(res);
  }

  async update(obj) {
    const id = optionalChain(() => obj._id);

    if (!id) {
      console.error(`Unable to update item: object has invalid id (${obj})`);
      return false;
    }

    const res = await this._apiClient.patch(`${this._basePath}/${id}`, obj);
    return this._checkGenericError(res);
  }

  async delete(id) {
    if (!id) {
      console.error(`Unable to delete item: given id is invalid (${id})`);
      return false;
    }

    const res = await this._apiClient.delete(`${this._basePath}/${id}`);
    return this._checkGenericError(res);
  }
}
