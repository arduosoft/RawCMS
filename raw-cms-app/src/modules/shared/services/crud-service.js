export class ICrudService {
  constructor() {}

  async getAll() {
    throw new Error(`Please Provide an implementation for ${this.getAll.name}`);
  }

  async getPage({ page = 1, size = 20, rawQuery = undefined } = {}) {
    throw new Error(`Please Provide an implementation for ${this.getPage.name}`);
  }

  async getById() {
    throw new Error(`Please Provide an implementation for ${this.getById.name}`);
  }

  async create(obj) {
    throw new Error(`Please Provide an implementation for ${this.create.name}`);
  }

  async update(obj) {
    throw new Error(`Please Provide an implementation for ${this.update.name}`);
  }

  async delete(id) {
    throw new Error(`Please Provide an implementation for ${this.delete.name}`);
  }
}
