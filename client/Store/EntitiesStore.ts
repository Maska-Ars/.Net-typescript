import { makeAutoObservable, runInAction } from "mobx";
import { $host } from "../Http";

interface Artist {
  id: string;
  name: string;
  country: string;
}

interface Release {
  id: string;
  name: string;
  image: string | null;
  dateAdded: string;
  artistId: string;
}

interface Record {
  id: string;
  name: string;
  durationSeconds: number;
  releaseId: string;
}

class EntitiesStore {
  artists: Artist[] = [];
  releases: Release[] = [];
  records: Record[] = [];

  loading: boolean = false;
  error: string | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  async fetchEntities() {
    this.loading = true;
    this.error = null;
    try {
      const [artistsResponse, releasesResponse, recordsResponse] =
        await Promise.all([
          $host.get<Artist[]>("/artist"),
          $host.get<Release[]>("/release"),
          $host.get<Record[]>("/record"),
        ]);

      runInAction(() => {
        this.artists = artistsResponse.data;
        this.releases = releasesResponse.data;
        this.records = recordsResponse.data;
      });
    } catch (err: any) {
      this.error = err.message || "Failed to fetch data";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }
  async fetchRecordsByReleaseId(releaseId: string) {
    this.loading = true;
    this.error = null;
    try {
      const response = await $host.get<Record[]>(
        `/record?releaseId=${releaseId}`
      );
      runInAction(() => {
        response.data.forEach((record) => {
          const existingIndex = this.records.findIndex(
            (r) => r.id === record.id
          );
          if (existingIndex !== -1) {
            this.records[existingIndex] = record;
          } else {
            this.records.push(record);
          }
        });
      });
    } catch (err: any) {
      this.error = err.message || "Failed to fetch records";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }
  async fetchArtist(id: string) {
    this.loading = true;
    this.error = null;
    try {
      const response = await axios.get<Artist>(`/artist/${id}`);
      runInAction(() => {
        const existingIndex = this.artists.findIndex((a) => a.id === id);
        if (existingIndex !== -1) {
          this.artists[existingIndex] = response.data;
        } else {
          this.artists.push(response.data);
        }
      });
    } catch (err: any) {
      this.error = err.message || "Failed to fetch artist";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }

  async addArtist(artist: Omit<Artist, "id">) {
    this.loading = true;
    this.error = null;
    try {
      const response = await $host.post<Artist>("/artist", artist);
      runInAction(() => {
        this.artists.push(response.data);
      });
    } catch (err: any) {
      this.error = err.message || "Failed to add artist";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }

  async updateArtist(id: string, artist: Omit<Artist, "id">) {
    this.loading = true;
    this.error = null;
    try {
      const response = await $host.patch<Artist>(`/artist/${id}`, artist);
      runInAction(() => {
        this.artists = this.artists.map((a) =>
          a.id === id ? response.data : a
        );
      });
    } catch (err: any) {
      this.error = err.message || "Failed to update artist";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }
  async deleteArtist(id: string) {
    this.loading = true;
    this.error = null;
    try {
      await $host.delete(`/artist/${id}`);
      runInAction(() => {
        this.artists = this.artists.filter((a) => a.id !== id);
      });
    } catch (err: any) {
      this.error = err.message || "Failed to delete artist";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }
  async addRelease(release: Omit<Release, "id" | "dateAdded">) {
    this.loading = true;
    this.error = null;
    try {
      const response = await $host.post<Release>("/release", release);
      runInAction(() => {
        this.releases.push(response.data);
      });
    } catch (err: any) {
      this.error = err.message || "Failed to add release";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }

  async updateRelease(id: string, release: Omit<Release, "id" | "dateAdded">) {
    this.loading = true;
    this.error = null;
    try {
      const response = await $host.patch<Release>(`/release/${id}`, release);
      runInAction(() => {
        this.releases = this.releases.map((r) =>
          r.id === id ? response.data : r
        );
      });
    } catch (err: any) {
      this.error = err.message || "Failed to update release";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }
  async deleteRelease(id: string) {
    this.loading = true;
    this.error = null;
    try {
      await $host.delete(`/release/${id}`);
      runInAction(() => {
        this.releases = this.releases.filter((r) => r.id !== id);
      });
    } catch (err: any) {
      this.error = err.message || "Failed to delete release";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }
  async addRecord(record: Omit<Record, "id">) {
    this.loading = true;
    this.error = null;
    try {
      const response = await $host.post<Record>("/record", record);
      runInAction(() => {
        this.records.push(response.data);
      });
    } catch (err: any) {
      this.error = err.message || "Failed to add record";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }

  async updateRecord(id: string, record: Omit<Record, "id">) {
    this.loading = true;
    this.error = null;
    try {
      const response = await $host.patch<Record>(`/record/${id}`, record);
      runInAction(() => {
        this.records = this.records.map((r) =>
          r.id === id ? response.data : r
        );
      });
    } catch (err: any) {
      this.error = err.message || "Failed to update record";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }

  async deleteRecord(id: string) {
    this.loading = true;
    this.error = null;
    try {
      await $host.delete(`/record/${id}`);
      runInAction(() => {
        this.records = this.records.filter((r) => r.id !== id);
      });
    } catch (err: any) {
      this.error = err.message || "Failed to delete record";
    } finally {
      runInAction(() => {
        this.loading = false;
      });
    }
  }
}

const entitiesStore = new EntitiesStore();
export default entitiesStore;
export type { Artist, Release, Record };
