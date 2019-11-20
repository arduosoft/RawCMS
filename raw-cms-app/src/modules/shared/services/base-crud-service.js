import { optionalChain } from '../../../utils/object.utils.js';
import { apiClient } from '../../core/api/api-client.js';
import { ICrudService } from './crud-service.js';

export class BaseCrudService extends ICrudService {
  _apiClient;
  _basePath;

  constructor({ basePath }) {
    super();
    this._apiClient = apiClient;
    this._basePath = basePath;
  }

  async getAll() {
    const res = await this.getPage({ size: Number.MAX_VALUE });
    return res.items;
  }

  async getPage({ page = 1, size = 20 } = {}) {
    const res = await this._apiClient.get(this._basePath, {
      params: { pageSize: size, pageNumber: page },
    });
    return res.data.data;
  }

  async getById(id) {
    if (!id) {
      console.error(`Unable to get item: id is invalid (${obj})`);
      return false;
    }

    const res = await this._apiClient.get(`${this._basePath}/${id}`);
    return res.data.data;
  }

  async create(obj) {
    const id = optionalChain(() => obj._id);

    if (id) {
      console.error(`Unable to create item: object has an id (${obj})`);
      return false;
    }

    const res = await this._apiClient.post(`${this._basePath}`, obj);
    return res.data.data === true;
  }

  async update(obj) {
    const id = optionalChain(() => obj._id);

    if (!id) {
      console.error(`Unable to update item: object has invalid id (${obj})`);
      return false;
    }

    const res = await this._apiClient.patch(`${this._basePath}/${id}`, obj);
    return res.data.data === true;
  }

  async delete(id) {
    if (!id) {
      console.error(`Unable to delete item: given id is invalid (${id})`);
      return false;
    }

    const res = await this._apiClient.delete(`${this._basePath}/${id}`);
    return res.data.data === true;
  }
}
