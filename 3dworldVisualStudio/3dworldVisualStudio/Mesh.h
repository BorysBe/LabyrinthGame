#pragma once

	class Mesh
	{
	private:
		float* vertices;
		int* indices;
		int sizeX;
		int sizeZ;
		int mapScale;

	public:
		Mesh(int sizeX, int sizeZ, int mapScale);
		float* getVerticesPointer();
		int* getIndicesPointer();
		void insertVertexAt(float posX, float height, float posZ);
	};


