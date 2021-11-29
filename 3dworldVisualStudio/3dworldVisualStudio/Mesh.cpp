#include "Mesh.h"

Mesh::Mesh(int sizeX, int sizeZ, int mapScale)
{
	this->sizeX = sizeX;
	this->sizeZ = sizeZ;
	this->mapScale = mapScale;
	vertices = new float[sizeX * sizeZ * 3];
	indices = new int[sizeX * sizeZ * 2];
}

float* Mesh::getVerticesPointer()
{
	return vertices;
}

int* Mesh::getIndicesPointer()
{
	return indices;
}

void Mesh::insertVertexAt(float posX, float height, float posZ)
{
	
}


