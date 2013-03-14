﻿/*
* Copyright (c) 2012-2013 AssimpNet - Nicholas Woodfield
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using Assimp.Unmanaged;

namespace Assimp {
    /// <summary>
    /// Represents a single bone of a mesh. A bone has a name which allows it to be found in the frame
    /// hierarchy and by which it can be addressed by animations. In addition it has a number of
    /// influences on vertices.
    /// </summary>
    public sealed class Bone {
        private String m_name;
        private VertexWeight[] m_weights;
        private Matrix4x4 m_offsetMatrix;

        /// <summary>
        /// Gets the name of the bone.
        /// </summary>
        public String Name {
            get {
                return m_name;
            }
        }

        /// <summary>
        /// Gets the number of vertex influences the bone contains.
        /// </summary>
        public int VertexWeightCount {
            get {
                return (m_weights == null) ? 0 : m_weights.Length;
            }
        }

        /// <summary>
        /// Gets if the bone has vertex weights - this should always be true.
        /// </summary>
        public bool HasVertexWeights {
            get {
                return m_weights != null;
            }
        }

        /// <summary>
        /// Gets the vertex weights owned by the bone.
        /// </summary>
        public VertexWeight[] VertexWeights {
            get {
                return m_weights;
            }
        }

        /// <summary>
        /// Gets the matrix that transforms from mesh space to bone space in bind pose.
        /// </summary>
        public Matrix4x4 OffsetMatrix
        {
            get
            {
                return m_offsetMatrix;
            }
        }

        /// <summary>
        /// Constructs a new Bone.
        /// </summary>
        /// <param name="bone">Unmanaged AiBone struct.</param>
        internal Bone(ref AiBone bone) {
            m_name = bone.Name.GetString();
            m_offsetMatrix = bone.OffsetMatrix;

            if(bone.NumWeights > 0 && bone.Weights != IntPtr.Zero) {
                m_weights = MemoryHelper.MarshalArray<VertexWeight>(bone.Weights, (int) bone.NumWeights);
            }
        }
    }
}
