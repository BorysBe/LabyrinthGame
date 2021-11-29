#include "GridFactory.h"
#include <iostream>

namespace grid_factory {

	Mesh GridFactory::CreateRandomTerrain(int sizeX, int sizeZ, int mapScale)
	{
		Mesh mesh(sizeX, sizeZ, mapScale);
		float* vertices = mesh.getVerticesPointer();
		int* indices = mesh.getIndicesPointer();

		float minX = -(sizeX / 2) * (mapScale / 2);
		float maxZ = (sizeZ / 2) * (mapScale / 2);

		int i = 0;
		for (int z = 0; z < sizeZ; z++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				vertices[i++] = minX + float(x) * mapScale;
				vertices[i++] = (((float)rand() - (float)rand()) / RAND_MAX) * 10.0f;
				vertices[i++] = maxZ - float(z) * mapScale;
			}
		}

		int j = 0;
		float currentVertex;
		for (int z = 0; z < sizeZ - 1; z++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				currentVertex = z * sizeX + x;
				indices[j++] = currentVertex + sizeX;
				indices[j++] = currentVertex;
			}
		}

		return mesh;
	}

	Mesh GridFactory::CreateFlatTerrain(int sizeX, int sizeZ, int mapScale)
	{
		Mesh mesh(sizeX, sizeZ, mapScale);
		float* vertices = mesh.getVerticesPointer();
		int* indices = mesh.getIndicesPointer();

		float minX = -(sizeX / 2) * (mapScale/2);
		float maxZ = (sizeZ / 2) * (mapScale/2);

		int i = 0;
		for (int z = 0; z < sizeZ; z++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				vertices[i++] = minX + float(x) * mapScale;
				vertices[i++] = 0;
				vertices[i++] = maxZ - float(z) * mapScale;
			}
		}

		int j = 0;
		float currentVertex;
		for (int z = 0; z < sizeZ-1; z++)
		{
			for (int x = 0; x < sizeX; x++)
			{
				currentVertex = z * sizeX + x;
				indices[j++] = currentVertex + sizeX;
				indices[j++] = currentVertex;
			}
		}

		return mesh;
	}



	//float* GridFactory::CreateCubeOnFlatTerrain(float* terrain, int sizeX, int sizeZ, float height)
	//{
	//	int middleXR = sizeX / 2;
	//	int middleXL = middleXR - 1;
	//	int middleZB = sizeZ / 2;
	//	int middleZU = middleZB - 1;

	//	//vertices to lift
	//	int cubeVertices[4] =
	//	{
	//		(middleXL + middleZU * sizeX) * 3,
	//		(middleXR + middleZU * sizeX) * 3,
	//		(middleXL + middleZB * sizeX) * 3,
	//		(middleXR + middleZB * sizeX) * 3
	//	};
	//	
	//	//vertices for wall flattening
	//	int cubeNeighborhood[12] = 
	//	{
	//		cubeVertices[0] - 3, cubeVertices[0] - 3 * sizeX, cubeVertices[0] - 3 * (sizeX + 1),
	//		cubeVertices[1] + 3, cubeVertices[1] - 3 * sizeX, cubeVertices[1] - 3 * (sizeX - 1),
	//		cubeVertices[2] - 3, cubeVertices[2] + 3 * sizeX, cubeVertices[2] + 3 * (sizeX - 1),
	//		cubeVertices[3] + 3, cubeVertices[3] + 3 * sizeX, cubeVertices[3] + 3 * (sizeX + 1)
	//	};

	//	// index to insert, valueX, valueZ
	//	/*float valuesToInsert[12][2];
	//	int insertAt[12];*/

	//	for(int i = 0; i < 4; i++)
	//	{
	//		//lifting
	//		terrain[cubeVertices[i] + 1] = height;

	//		int neighbourIndex = i * 3;
	//		for(int j = 0; j < 3; j++)
	//		{
	//			int currentVertex = neighbourIndex + j;
	//			//storing values that will be replaced and their indexes
	//		/*	insertAt[currentVertex] = cubeNeighborhood[currentVertex] + currentVertex * 3;
	//			valuesToInsert[currentVertex][0] = terrain[cubeNeighborhood[currentVertex]];
	//			valuesToInsert[currentVertex][1] = terrain[cubeNeighborhood[currentVertex]];*/

	//			//flattening the walls
	//			terrain[cubeNeighborhood[currentVertex]] = terrain[cubeVertices[i]];
	//			terrain[cubeNeighborhood[currentVertex] + 2] = terrain[cubeVertices[i]+2];
	//		}
	//	}

	//	

	//	return terrain;
	//}

}
