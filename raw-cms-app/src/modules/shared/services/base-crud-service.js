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
    throw new Error('To be implemented. This should return all values.');
  }

  async getPage({ page = 0 } = {}) {
    // FIXME: Handle pagination (maybe in api client?)
    const res = await this._apiClient.get(this._basePath);
    return res.data.data.items;
  }

  async getById(id) {
    if (!id) {
      console.error(`Unable to get item: id is invalid (${obj})`);
      return false;
    }

    const res = await this._apiClient.get(`${this._basePath}/${id}`);
    return res.data.data;
  }

  async create() {
    throw new Error('To be implemented. This should return the created item.');
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
